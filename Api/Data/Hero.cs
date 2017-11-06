using System.ComponentModel.DataAnnotations;


namespace Api.Data
{

    public class Hero
    {

        public long Id
        {
            get;

            set;
        }


        [Required]
        public string Name
        {
            get;

            set;
        }

    }

}
