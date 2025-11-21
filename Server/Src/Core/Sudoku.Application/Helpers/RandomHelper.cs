using System;
using System.IO;
using System.Linq;

namespace Sudoku.Application.Helpers;

public static class RandomHelper
{
    public static string Getstring(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var random = new Random();
        return new string([.. Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)])]);
    }
    public static string GetProfileImage()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-images");

        if (!Directory.Exists(path))
            return null;

        var files = Directory.GetFiles(path);

        if (files.Length == 0)
            return null;

        var rnd = new Random();
        var randomFile = files[rnd.Next(files.Length)];

        return Path.GetFileName(randomFile);
    }
}
