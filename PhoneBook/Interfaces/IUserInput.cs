namespace PhoneBook.Interfaces
{
    internal interface IUserInput
    {
        string GetEmailInput();
        string GetNameInput();
        string GetNumberInput(string message);
    }
}