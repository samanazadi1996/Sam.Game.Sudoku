using System;
using System.Collections.Generic;

namespace Sudoku.Application.Helpers;

public static class Messages
{
    public static class UserGameMessages
    {
        public static string NotesLimitReached() => "حداکثر تعداد یادداشت‌ها برای این خانه ثبت شده است.";
        public static string ActiveGameNotFound()    => "بازی فعال برای کاربر یافت نشد.";

    }
    public static class RoleMessages
    {
        public static string ThisRolesNotFound(string roles) => $"این نقش‌ها وجود ندارند : {roles}";
        public static string RoleNotFound(Guid role) => $"این نقش‌ وجود ندارد : {role}";
    }

    public static class AccountMessages
    {
        public static string Account_NotFound_with_UserName(string userName) => $"کاربری با نام کاربری {userName} یافت نشد";
        public static string Username_is_already_taken(string userName) => $"نام کاربری {userName} قبلاً ثبت شده است";
        public static string Invalid_password() => "رمز عبور نامعتبر است";

        public static string UserNameAlreadyExists(string userName) => $"کاربر با نام کاربری {userName} وجود دارد";
    }

}
public static class IdGenerator
{
    public static Guid Generate()
    {
        return Guid.NewGuid();
    }
}