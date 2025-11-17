namespace Sudoku.Application.Interfaces;

public interface ICryptographyServices
{
    string Hash(string stringToHash);

    string AesEncrypt(string text);
    string AesDecrypt(string text);

}