using FluentValidation;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Validators
{
    /// <summary>
    /// Валидация <see cref="PassportCreateModel"/>
    /// </summary>
    public class PassportCreateModelValidator : AbstractValidator<PassportCreateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PassportCreateModelValidator()
        {

        }
    }
}