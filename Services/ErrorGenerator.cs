using Bogus;
using FakeGenerator.Helpers;
using FakeGenerator.Models;
using System.Text;

namespace FakeGenerator.Services
{
    public class ErrorGenerator
    {
        private readonly Action<User, FakeDataConfigurationViewModel>[] _errorMethods;

        public ErrorGenerator()
        {
            _errorMethods = new[]
            {
                DeleteRandomSymbol,
                AddRandomSymbol,
                SwapRandomSymbols
            };
        }

        public void ApplyErrors(List<User> users, FakeDataConfigurationViewModel? configuration)
        {
            foreach (var user in users)
            {
                ApplyErrorsForWholeErrors(configuration, user);
                ApplyErrorForFractionalPart(configuration, user);
            }
        }

        private void ApplyErrorsForWholeErrors(FakeDataConfigurationViewModel? configuration, User user)
        {
            for (var i = 0; i < configuration.ErrorCount; i++)
            {
                ApplyError(configuration, user);
            }
        }

        private void ApplyErrorForFractionalPart(FakeDataConfigurationViewModel? configuration, User user)
        {
            if (GetFractionalPart(configuration.ErrorCount) != 0 &&
                Randomizer.Seed.NextDouble() > GetFractionalPart(configuration.ErrorCount))
            {
                ApplyError(configuration, user);
            }
        }

        private void ApplyError(FakeDataConfigurationViewModel configuration, User user)
        {
            var randomMethod = _errorMethods[Randomizer.Seed.Next(_errorMethods.Length)];
            randomMethod(user, configuration);
        }

        private void DeleteRandomSymbol(User user, FakeDataConfigurationViewModel? configuration) =>
            ApplyActionToField(user, DeleteRandomSymbolInField);

        private void AddRandomSymbol(User user, FakeDataConfigurationViewModel configuration) =>
            ApplyActionToField(user, input => AddRandomSymbolToField(input, configuration.Region));

        private void SwapRandomSymbols(User user, FakeDataConfigurationViewModel configuration) =>
            ApplyActionToField(user, SwapAdjacentSymbolsInField);

        private void ApplyActionToField(User user, Func<string, string> action)
        {
            var fieldChoice = Randomizer.Seed.Next(3);
            switch (fieldChoice)
            {
                case 0:
                    user.FullName = action(user.FullName);
                    break;
                case 1:
                    user.Address = action(user.Address);
                    break;
                case 2:
                    user.Phone = action(user.Phone);
                    break;
            }
        }

        private string DeleteRandomSymbolInField(string? input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var indexToRemove = Randomizer.Seed.Next(input.Length);
            return RemoveCharAtIndex(input, indexToRemove);
        }

        private string AddRandomSymbolToField(string input, string region)
        {
            var localLettersSet = LocalLettersSetProvider.GetLettersSet(region);
            var randomChar = localLettersSet[Randomizer.Seed.Next(localLettersSet.Length)];
            var indexToInsert = Randomizer.Seed.Next(input.Length + 1);
            return InsertCharAtIndex(input, indexToInsert, randomChar);
        }

        private string SwapAdjacentSymbolsInField(string? input)
        {
            if (input.Length < 2) return input;
            var indexToSwap = Randomizer.Seed.Next(input.Length - 1);
            var inputChars = input.ToCharArray();
            SwapTwoChars(inputChars, indexToSwap, indexToSwap + 1);
            return new string(inputChars);
        }

        private string RemoveCharAtIndex(string input, int index)
        {
            var stringBuilder = new StringBuilder(input);
            stringBuilder.Remove(index, 1);
            return stringBuilder.ToString();
        }

        private string InsertCharAtIndex(string input, int index, char character)
        {
            var stringBuilder = new StringBuilder(input);
            stringBuilder.Insert(index, character);
            return stringBuilder.ToString();
        }

        private void SwapTwoChars(char[] input, int index1, int index2)
        {
            var temp = input[index1];
            input[index1] = input[index2];
            input[index2] = temp;
        }

        private double GetFractionalPart(double value) => value - Math.Truncate(value);
    }
}