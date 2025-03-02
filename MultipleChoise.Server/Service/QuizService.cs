using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Contract.Dto.UserDto;
using MultipleChoise.Server.Contract.Share.Enum;
using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Data.UnitOfWork;
using MultipleChoise.Server.Service.Interface;

namespace MultipleChoise.Server.Service
{
    public class QuizService(IUnitOfWork unitOfWork, IMapper mapper) : ICrudService<QuizDto>, IQuizService
    {
        public async Task<QuizDto?> GetByIdAsync(Guid id)
        {
            return mapper.Map<QuizDto?>(await unitOfWork.QuizRepo.GetQuizWithQuestionsAndAnswersAsync(id));
        }

        private bool IsQuizReady(Quiz? quiz)
        {
            return quiz?.Questions?.All(q => q.Answers.Any(a => a.IsCorrect)) == true;
        }

        public async Task<IEnumerable<QuizDto>> GetAllAsync()
        {
            var allQuiz = mapper.Map<IEnumerable<QuizDto>>(await unitOfWork.QuizRepo.GetQuizzesWithQuestionsAsync());
            foreach (var quiz in allQuiz)
            {
                var dbQuiz = await unitOfWork.QuizRepo.GetQuizWithQuestionsAndAnswersAsync(quiz.Id);
                quiz.Status = IsQuizReady(dbQuiz) ? QuizStatus.Ready : QuizStatus.Pending;
            }

            return allQuiz;
        }

        public async Task CreateAsync(QuizDto quiz)
        {
            await unitOfWork.QuizRepo.AddAsync(mapper.Map<Quiz>(quiz));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<QuizDto> CreateAndReturnAsync(QuizDto quiz)
        {
            var quizSaved = await unitOfWork.QuizRepo.CreateQuizAsync(mapper.Map<Quiz>(quiz));
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<QuizDto>(quizSaved);
        }

        public async Task UpdateAsync(QuizDto quiz)
        {
            if (quiz?.Id == null)
            {
                return;
            }

            unitOfWork.QuizRepo.Update(mapper.Map<Quiz>(quiz));

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var quiz = await unitOfWork.QuizRepo.GetByIdAsync(id);
            if (quiz == null)
            {
                return;
            }
            unitOfWork.QuizRepo.Delete(quiz);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<QuizUserDto?> GetByNumberAsync(int number)
        {
            var quiz = await unitOfWork.QuizRepo.GetQuizByNumberAsync(number);
            if (IsQuizReady(quiz))
            {
                return mapper.Map<QuizUserDto?>(quiz);
            }

            throw new Exception(" Quiz isn't ready");
        }

        public async Task<QuizDto> UpdateQuizAsync(QuizDto quizDto)
        {
            if (quizDto?.Id == null)
            {
                throw new ArgumentException("Quiz ID is required.");
            }

            var dbQuiz = await unitOfWork.QuizRepo.GetQuizWithQuestionsAndAnswersAsync(quizDto.Id);
            if (dbQuiz == null)
            {
                throw new ArgumentException("Quiz not found.");
            }

            dbQuiz.Title = quizDto.Title;
            dbQuiz.Duration = TimeSpan.FromMinutes(quizDto.DurationInMinutes);

            var dbQuestionIds = dbQuiz.Questions.Select(q => q.Id).ToHashSet();
            var newQuestionIds = quizDto.Questions.Select(q => q.Id).ToHashSet();

            var addedQuestions = quizDto.Questions
                .Where(q => !dbQuestionIds.Contains(q.Id))
                .Select(q => new Question
                {
                    Id = Guid.NewGuid(),
                    Content = q.Content,
                    QuizId = dbQuiz.Id,
                    Answers = q.Answers.Select(a => new Answer
                    {
                        Id = Guid.NewGuid(),
                        Content = a.Content,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList();

            var deletedQuestions = dbQuiz.Questions
                .Where(q => !newQuestionIds.Contains(q.Id))
                .ToList();

            foreach (var question in deletedQuestions)
            {
                unitOfWork.QuestionRepo.Delete(question);
            }

            if (addedQuestions.Any())
            {
                dbQuiz.Questions ??= new List<Question>();
                foreach (var question in addedQuestions)
                {
                    question.QuizId = dbQuiz.Id;
                    question.Id = Guid.NewGuid();
                    await unitOfWork.QuestionRepo.AddAsync(question);
                    //dbQuiz.Questions.Append(question);
                }
            }

            foreach (var question in dbQuiz.Questions)
            {
                var updatedQuestion = quizDto.Questions.FirstOrDefault(q => q.Id == question.Id);
                if (updatedQuestion != null)
                {
                    question.Content = updatedQuestion.Content;

                    var dbAnswerIds = question.Answers.Select(a => a.Id).ToHashSet();
                    var newAnswerIds = updatedQuestion.Answers.Select(a => a.Id).ToHashSet();

                    var addedAnswers = updatedQuestion.Answers
                        .Where(a => !dbAnswerIds.Contains(a.Id))
                        .Select(a => new Answer
                        {
                            Id = Guid.NewGuid(),
                            Content = a.Content,
                            IsCorrect = a.IsCorrect,
                            QuestionId = question.Id
                        }).ToList();

                    var deletedAnswers = question.Answers
                        .Where(a => !newAnswerIds.Contains(a.Id))
                        .ToList();

                    if (addedAnswers.Any())
                    {
                        question.Answers ??= new List<Answer>();
                        foreach (var answer in addedAnswers)
                        {
                            await unitOfWork.AnswerRepo.AddAsync(answer);
                        }
                    }

                    foreach (var answer in deletedAnswers)
                    {
                        unitOfWork.AnswerRepo.Delete(answer);
                    }

                    foreach (var answer in question.Answers)
                    {
                        var updatedAnswer = updatedQuestion.Answers.FirstOrDefault(a => a.Id == answer.Id);
                        if (updatedAnswer != null)
                        {
                            answer.Content = updatedAnswer.Content;
                            answer.IsCorrect = updatedAnswer.IsCorrect;
                            //unitOfWork.AnswerRepo.Update(answer);
                        }
                    }

                    //unitOfWork.QuestionRepo.Update(question);
                }
            }

            try
            {
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Handle exception
            }

            var updatedQuiz = await unitOfWork.QuizRepo.GetQuizWithQuestionsAndAnswersAsync(quizDto.Id);
            return mapper.Map<QuizDto?>(updatedQuiz);
        }


        //public async Task<QuizDto> UpdateQuizAsync(QuizDto quizDto)
        //{
        //    if (quizDto?.Id == null)
        //    {
        //        throw new ArgumentException("Quiz id is required.");
        //    }

        //    var dbQuiz = await unitOfWork.QuizRepo
        //        .GetQuizWithQuestionsAndAnswersAsync(quizDto.Id); // Load full quiz with questions and answers

        //    if (dbQuiz == null)
        //    {
        //        throw new ArgumentException("Quiz not found.");
        //    }

        //    dbQuiz.Title = quizDto.Title;
        //    dbQuiz.Duration = TimeSpan.FromMinutes(quizDto.DurationInMinutes);

        //    // compare questions
        //    var dbQuestionIds = dbQuiz.Questions.Select(q => q.Id).ToList();
        //    var newQuestionIds = quizDto.Questions.Select(q => q.Id).ToList();

        //    // Added questions
        //    var addedQuestions = quizDto.Questions
        //        .Where(q => !dbQuestionIds.Contains(q.Id))
        //        .Select(q => new Question
        //        {
        //            Content = q.Content,
        //            QuizId = dbQuiz.Id,
        //            Answers = q.Answers.Select(a => new Answer
        //            {
        //                Content = a.Content,
        //                IsCorrect = a.IsCorrect,
        //                QuestionId = q.Id
        //            }).ToList()
        //        })
        //        .ToList();
        //    foreach (var question in addedQuestions)
        //    {
        //        dbQuiz.Questions.Add(question);
        //    }

        //    // 🔴 Câu hỏi bị xóa
        //    var deletedQuestions = dbQuiz.Questions
        //        .Where(q => !newQuestionIds.Contains(q.Id))
        //        .ToList();
        //    foreach (var question in deletedQuestions)
        //    {
        //        unitOfWork.QuestionRepo.Delete(question);
        //    }

        //    // 🟡 Cập nhật câu hỏi
        //    foreach (var question in dbQuiz.Questions)
        //    {
        //        var updatedQuestion = quizDto.Questions.FirstOrDefault(q => q.Id == question.Id);
        //        if (updatedQuestion != null)
        //        {
        //            question.Content = updatedQuestion.Content;

        //            // So sánh câu trả lời
        //            var dbAnswerIds = question.Answers.Select(a => a.Id).ToList();
        //            var newAnswerIds = updatedQuestion.Answers.Select(a => a.Id).ToList();

        //            // 🟢 Câu trả lời mới
        //            var addedAnswers = updatedQuestion.Answers
        //                .Where(a => !dbAnswerIds.Contains(a.Id))
        //                .Select(a => new Answer
        //                {
        //                    Content = a.Content,
        //                    IsCorrect = a.IsCorrect,
        //                    QuestionId = question.Id
        //                })
        //                .ToList();
        //            foreach (var answer in addedAnswers)
        //            {
        //                question.Answers.Add(answer);
        //            }

        //            // 🔴 Câu trả lời bị xóa
        //            var deletedAnswers = question.Answers
        //                .Where(a => !newAnswerIds.Contains(a.Id))
        //                .ToList();
        //            foreach (var answer in deletedAnswers)
        //            {
        //                unitOfWork.AnswerRepo.Delete(answer);
        //            }

        //            // 🟡 Cập nhật câu trả lời
        //            foreach (var answer in question.Answers)
        //            {
        //                var updatedAnswer = updatedQuestion.Answers.FirstOrDefault(a => a.Id == answer.Id);
        //                if (updatedAnswer != null)
        //                {
        //                    answer.Content = updatedAnswer.Content;
        //                    answer.IsCorrect = updatedAnswer.IsCorrect;
        //                }
        //            }
        //        }
        //    }
        //    //unitOfWork.QuizRepo.Update(dbQuiz);
        //    await unitOfWork.SaveChangesAsync();
        //    var t = await unitOfWork.QuizRepo.GetQuizWithQuestionsAndAnswersAsync(quizDto.Id);
        //    return mapper.Map<QuizDto?>(t);
        //}

        //public async Task UpdateQuizAsync(QuizDto quizDto)
        //{
        //    var existingQuiz = await unitOfWork.QuizRepo.GetQuizWithQuestionsAndAnswersAsync(quizDto.Id);

        //    if (existingQuiz == null)
        //    {
        //        throw new KeyNotFoundException("Quiz not found");
        //    }

        //    // Cập nhật thông tin của Quiz
        //    existingQuiz.Title = quizDto.Title;
        //    existingQuiz.Description = quizDto.Description;
        //    existingQuiz.UpdatedAt = DateTime.UtcNow;

        //    // Danh sách câu hỏi hiện tại trong DB
        //    var existingQuestionIds = existingQuiz.Questions.Select(q => q.Id).ToList();
        //    var newQuestionIds = quizDto.Questions.Select(q => q.Id).ToList();

        //    // Xóa câu hỏi không còn trong danh sách mới
        //    var questionsToRemove = existingQuiz.Questions
        //        .Where(q => !newQuestionIds.Contains(q.Id))
        //        .ToList();
        //    _unitOfWork.QuestionRepository.RemoveRange(questionsToRemove);

        //    foreach (var questionDto in quizDto.Questions)
        //    {
        //        var existingQuestion = existingQuiz.Questions.FirstOrDefault(q => q.Id == questionDto.Id);

        //        if (existingQuestion == null) // Thêm mới câu hỏi
        //        {
        //            var newQuestion = new Question
        //            {
        //                Id = Guid.NewGuid(),
        //                QuizId = existingQuiz.Id,
        //                Content = questionDto.Content,
        //                Answers = questionDto.Answers.Select(a => new Answer
        //                {
        //                    Id = Guid.NewGuid(),
        //                    Content = a.Content,
        //                    IsCorrect = a.IsCorrect // Chỉ admin mới gửi dữ liệu này
        //                }).ToList()
        //            };
        //            existingQuiz.Questions.Add(newQuestion);
        //        }
        //        else // Cập nhật câu hỏi
        //        {
        //            existingQuestion.Content = questionDto.Content;

        //            // Danh sách Answer hiện tại
        //            var existingAnswerIds = existingQuestion.Answers.Select(a => a.Id).ToList();
        //            var newAnswerIds = questionDto.Answers.Select(a => a.Id).ToList();

        //            // Xóa các Answer không còn tồn tại
        //            var answersToRemove = existingQuestion.Answers
        //                .Where(a => !newAnswerIds.Contains(a.Id))
        //                .ToList();
        //            _unitOfWork.AnswerRepository.RemoveRange(answersToRemove);

        //            foreach (var answerDto in questionDto.Answers)
        //            {
        //                var existingAnswer = existingQuestion.Answers.FirstOrDefault(a => a.Id == answerDto.Id);
        //                if (existingAnswer == null) // Thêm mới Answer
        //                {
        //                    var newAnswer = new Answer
        //                    {
        //                        Id = Guid.NewGuid(),
        //                        QuestionId = existingQuestion.Id,
        //                        Content = answerDto.Content,
        //                        IsCorrect = answerDto.IsCorrect // Kiểm tra quyền trước khi cập nhật
        //                    };
        //                    existingQuestion.Answers.Add(newAnswer);
        //                }
        //                else // Cập nhật Answer
        //                {
        //                    existingAnswer.Content = answerDto.Content;
        //                    existingAnswer.IsCorrect = answerDto.IsCorrect; // Kiểm tra quyền trước khi cập nhật
        //                }
        //            }
        //        }
        //    }

        //    _unitOfWork.QuizRepository.Update(existingQuiz);
        //    await _unitOfWork.SaveChangesAsync();
        //}



    }
}
