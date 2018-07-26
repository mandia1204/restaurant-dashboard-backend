namespace Models
{
    public class ChartRow<K,V>
    {
        public string Group { get; set; }
        public K Key { get; set; }
        public V Value { get; set; }
    }
}