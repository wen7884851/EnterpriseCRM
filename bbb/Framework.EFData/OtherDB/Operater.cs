//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;


namespace Framework.EFData.OtherDB
{
    public class Operater
    {
        public Operater()
        {
            this.Name = "Anonymous";
        }

        public string Name { get; set; }

        public string IP { get; set; }
        public DateTime Time { get; set; }
        public Guid Token { get; set; }
        public int UserId { get; set; }
        public string Method { get; set; }
    }
}
