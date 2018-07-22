namespace Models {
    public class Card<V> {
        public V Value { get; set; }

    }

    public class Card<V1, V2> {
        public V1 Value { get; set; }
        public V2 Value2 { get; set; }
    }
}