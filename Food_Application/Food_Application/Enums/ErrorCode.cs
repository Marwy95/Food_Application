namespace Food_Application.Enums
{
    public enum ErrorCode
    {
        NoError,
        //1-100 User
        UnKnown = 1,
        PasswordsDontMatch =1,
        UserNameAlreadyExist = 2,
        EmailAlreadyExist = 3,
        WrongPasswordOrEmail=4,
        EmailIsNotFound =5,
        UserNameIsNotFound = 6,

    }
}
