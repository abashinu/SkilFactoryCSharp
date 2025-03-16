using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Заполните анкету");
            var userData = GetUserData();
            DisplayUserData(userData);
            bool repeat = GetValidatedInput("Повторить ввод данных? (Да/Нет): ", input => ValidateYesNoInput(input)) == "Да";
            if (!repeat)
                break;
        }
    }

    static (string Name, string Surname, int Age, bool HasPet, string[] PetNames, string[] FavoriteColors) GetUserData()
    {
        string name = GetValidatedInput("Введите ваше имя: ", input => ValidateNameInput(input));
        string surname = GetValidatedInput("Введите вашу фамилию: ", input => ValidateNameInput(input));
        int age = int.Parse(GetValidatedInput("Введите ваш возраст: ", input => ValidatePosNumInput(input)));
        bool hasPet = GetValidatedInput("У вас есть питомец? (Да/Нет): ", input => ValidateYesNoInput(input)) == "Да";
        string[] petNames = hasPet ? GetPetNames() : new string[0];
        int favoriteColorsCount = int.Parse(GetValidatedInput("Введите количество ваших любимых цветов:", input => ValidatePosZeroNumInput(input)));
        string[] favoriteColors = GetFavoriteColors(favoriteColorsCount);

        return (name, surname, age, hasPet, petNames, favoriteColors);
    }

    private static string ValidateNameInput(string input)
    {
        if (!string.IsNullOrWhiteSpace(input) && !int.TryParse(input, out _))
            return "";
        else
            return "Значение не может быть пустым или числом.";
    }

    private static string ValidatePosNumInput(string input)
    {
        if (int.TryParse(input, out int number) && (number > 0))
            return "";
        else
            return "Укажите положительное число.";
    }

    private static string ValidatePosZeroNumInput(string input)
    {
        if (int.TryParse(input, out int number) && (number >= 0))
            return "";
        else
            return "Укажите не отрицательное число.";
    }

    static string ValidateYesNoInput(string prompt)
    {
        if ((prompt == "Да") || (prompt == "Нет"))
            return "";
        else
            return "Пожалуйста, введите 'Да' или 'Нет'.";
    }

    static string GetValidatedInput(string prompt, Func<string, string> validationFunc)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            string errorMessage = validationFunc(input);
            if (errorMessage == "")
            {
                return input;
            }
            Console.WriteLine(errorMessage);
        }
    }

    static string[] GetPetNames()
    {
        int petCount = int.Parse(GetValidatedInput("Введите количество питомцев:", input => ValidatePosNumInput(input)));
        string[] petNames = new string[petCount];
        for (int i = 0; i < petCount; i++)
        {
            petNames[i] = GetValidatedInput($"Введите кличку питомца {i + 1}:", input => ValidateNameInput(input));
        }
        return petNames;
    }

    static string[] GetFavoriteColors(int count)
    {
        string[] colors = new string[count];
        for (int i = 0; i < count; i++)
        {
            colors[i] = GetValidatedInput("Введите любимый цвет:", input => ValidateNameInput(input));
        }
        return colors;
    }

    static void DisplayUserData((string Name, string Surname, int Age, bool HasPet, string[] PetNames, string[] FavoriteColors) userData)
    {
        Console.WriteLine("\nДанные пользователя:");
        Console.WriteLine($"Имя: {userData.Name}");
        Console.WriteLine($"Фамилия: {userData.Surname}");
        Console.WriteLine($"Возраст: {userData.Age}");
        Console.WriteLine($"Наличие питомца: {(userData.HasPet ? "Да" : "Нет")}");

        if (userData.HasPet)
        {
            Console.WriteLine("Клички питомцев:");
            foreach (var petName in userData.PetNames)
            {
                Console.WriteLine($"- {petName}");
            }
        }

        Console.WriteLine("Любимые цвета:");
        foreach (var color in userData.FavoriteColors)
        {
            Console.WriteLine($"- {color}");
        }
    }
}
