namespace Biblioteca.Client.Helpers;

public class EnumOption<T>
{
    public string Text { get; set; } = string.Empty;
    public T Value { get; set; }
}