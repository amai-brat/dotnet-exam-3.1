namespace TicTacToe.AuthService.Validation;

public static class ValidationMessages
{
    public const string LoginRequired = "Логин обязателен";
    public const string PasswordRequired = "Пароль обязателен";
    
    public const string PasswordMustHaveDigit = "Пароль должен содержать цифру";
    public const string PasswordMustHaveLowerCaseLetter = "Пароль должен содержать строчную букву";
    public const string PasswordMustHaveUpperCaseLetter = "Пароль должен содержать заглавную букву";
    public const string PasswordMustHaveEnoughLength = "Длина пароля должна быть не меньше 7";
    
    public const string PasswordMustMatch = "Пароли должны совпадать";
}