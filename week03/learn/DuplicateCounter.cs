public class DuplicateCounter
{
    //Count how many duplicates are in a collection of data.

    public int CountDuplicates(List<int> numbers)
    {
        HashSet<int> seen = new HashSet<int>();
        HashSet<int> duplicates = new HashSet<int>();

        foreach (int number in numbers)
        {
            if (!seen.Add(number))
            {
                duplicates.Add(number);
            }
        }

        return duplicates.Count;
    }
}