using FluentValidation.TestHelper;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Validators;

namespace TravelBot.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="CategoryCreateModelValidator"/>
    /// </summary>
    public class CategoryCreateModelValidatorTests
    {
        private readonly CategoryCreateModelValidator validator = new();

        [Fact]
        public void ShouldHaveErrorWhenNameIsEmpty()
        {
            // Arrange
            var model = new CategoryCreateModel
            {
                Name = string.Empty
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void ShouldHaveErrorWhenNameIsWhitespace()
        {
            // Arrange
            var model = new CategoryCreateModel
            {
                Name = "   "
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void ShouldHaveErrorWhenNameIsTooShort()
        {
            // Arrange
            var model = new CategoryCreateModel
            {
                Name = "a"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void ShouldHaveErrorWhenNameIsTooLong()
        {
            // Arrange
            var model = new CategoryCreateModel
            {
                Name = new string('a', 256)
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void ShouldNotHaveErrorsWhenModelIsValid()
        {
            // Arrange
            var model = new CategoryCreateModel
            {
                Name = "Museums"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}