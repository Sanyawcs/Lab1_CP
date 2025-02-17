using System;
using System.Runtime.InteropServices;
using System.Text;

class Program
{
    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern bool GetComputerName(StringBuilder lpBuffer, ref int nSize);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
    public static extern bool GetUserName(StringBuilder lpBuffer, ref int nSize);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern uint GetSystemDirectory(StringBuilder lpBuffer, uint uSize);

    [DllImport("kernel32.dll")]
    public static extern uint GetVersion();

    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(int nIndex);

    [DllImport("kernel32.dll")]
    public static extern void GetSystemTime(ref SYSTEMTIME st);

    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }

    static void Main(string[] args)
    {
        // Получение имени компьютера
        StringBuilder computerName = new StringBuilder(256);
        int computerNameLength = computerName.Capacity;
        GetComputerName(computerName, ref computerNameLength);

        // Получение имени пользователя
        StringBuilder userName = new StringBuilder(256);
        int userNameLength = userName.Capacity;
        GetUserName(userName, ref userNameLength);

        // Получение системной директории
        StringBuilder systemDirectory = new StringBuilder(256);
        GetSystemDirectory(systemDirectory, (uint)systemDirectory.Capacity);

        // Получение версии ОС
        uint version = GetVersion();
        string osVersion = $"{(version & 0xFF)}.{(version >> 8) & 0xFF}";

        // Получение разрешения экрана
        int screenWidth = GetSystemMetrics(0); // SM_CXSCREEN
        int screenHeight = GetSystemMetrics(1); // SM_CYSCREEN

        // Получение текущей даты и времени
        SYSTEMTIME st = new SYSTEMTIME();
        GetSystemTime(ref st);
        DateTime currentDateTime = new DateTime(st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond);

        DateTime localDateTime = currentDateTime.ToLocalTime();

        // Вывод информации
        Console.WriteLine($"Имя компьютера: {computerName}");
        Console.WriteLine($"Имя пользователя: {userName}");
        Console.WriteLine($"Системная директория: {systemDirectory}");
        Console.WriteLine($"Версия ОС: {osVersion}");
        Console.WriteLine($"Разрешение экрана: {screenWidth}x{screenHeight}");
        Console.WriteLine($"Текущая дата и время: {localDateTime}");
    }
}
