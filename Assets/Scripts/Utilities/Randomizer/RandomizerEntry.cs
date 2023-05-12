namespace ProjectCatch.Utilities
{
    public class RandomizerEntry<T>
    {
        public virtual T Entry { get; }
        
        public RandomizerEntry(T entry)
        {
            Entry = entry;
        }
    }
}
