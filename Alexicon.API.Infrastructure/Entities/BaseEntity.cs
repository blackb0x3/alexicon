namespace Alexicon.API.Infrastructure.Entities;

public abstract class BaseEntity
{
    public const char ForDbDelimiter = ',';

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }
}