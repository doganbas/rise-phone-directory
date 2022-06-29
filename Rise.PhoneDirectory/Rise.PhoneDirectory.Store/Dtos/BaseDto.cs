namespace Rise.PhoneDirectory.Store.Dtos
{
    public class BaseDto<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
