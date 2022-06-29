namespace Rise.PhoneDirectory.Store.Dtos
{
    public class PersonDto : BaseDto<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string CompanyName { get; set; }
    }
}