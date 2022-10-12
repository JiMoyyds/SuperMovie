namespace SuperMovie.Db.Vip.Model
{
    public class VipModel : IVipModel
    {
        public long Level { get; set; }
        public double Discount { get; set; }
        public double MonthPrice { get; set; }
        public VipModel(long level, double discount, double monthPirce)
        {
            this.Level = level;
            this.Discount = discount;
            this.MonthPrice = monthPirce;
        }
    }
}
