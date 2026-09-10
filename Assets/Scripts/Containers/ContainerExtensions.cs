using System;

public static class ContainerExtensions
{
    /// <summary>
    /// Attempts to transfer an item from source to destination.
    /// </summary>
    /// <returns>True if the item was successfully transferred; false if the source was empty or destination was full.</returns>
    /// <exception cref="InvalidOperationException">Thrown when rollback fails, preventing silent data loss.</exception>
    public static bool TryTransferTo(this IContainer source, IContainer destination)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (destination == null)
        {
            throw new ArgumentNullException(nameof(destination));
        }

        if (ReferenceEquals(source, destination))
        {
            return false;
        }

        if (!source.TryRetrieve(out var item))
        {
            return false;
        }

        if (destination.TryStore(item))
        {
            return true;
        }

        if (!source.TryStore(item)) // Rollback attempt
        {
            throw new InvalidOperationException(
                $"Failed to store {item.name} back into source after destination store failed."
            );
        }

        return false;
    }
}