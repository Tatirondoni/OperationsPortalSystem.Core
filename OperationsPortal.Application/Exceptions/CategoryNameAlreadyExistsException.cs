namespace OperationsPortal.Application.Exceptions;

public sealed class CategoryNameAlreadyExistsException : Exception
{
    public CategoryNameAlreadyExistsException(string name)
        : base($"A categoria '{name}' já existe.")
    {
        Name = name;
    }

    public string Name { get; }
}
