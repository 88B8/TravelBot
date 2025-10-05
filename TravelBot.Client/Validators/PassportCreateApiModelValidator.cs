using FluentValidation;
using TravelBot.Client.Contracts.Client;

namespace TravelBot.Client.Validators
{
    /// <summary>
    /// Валидатор <see cref="PassportCreateApiModel"/>
    /// </summary>
    public class PassportCreateApiModelValidator : AbstractValidator<PassportCreateApiModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PassportCreateApiModelValidator()
        {

        }
    }
}
