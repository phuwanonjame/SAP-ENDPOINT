using MyApi.Models;

namespace MyApi.Services
{
    public class BudgetService : IBudgetService
    {
        public async Task<Budget> GetBudgetAsync()
        {
            await Task.Delay(50); // จำลอง async

            return new Budget
            {
                IFlgCheckonly = "",
                Bltyp = "030",
                Bldat = "20250902",
                Budat = "20250902",
                Xblnr = "PC ONLINE",
                Waers = "THB",
                Bukrs = "5100",
                Ktext = "PC20250902001",
                Blpos = "001",
                Wrbtr = "10.00",
                Dmbtr = "10.00",
                Ptext = "ค่าโทรศัพท์ ประจำเดือน 8/68 NKM",
                Saknr = "6201101",
                PsPspPnr = "51E.5105003021.6201100",
                Kostl = "5105003021",
                MessageType = "S",
                Message = "สามารถ Lock Budget ได้",
                Belnr = "5100000122"
            };
        }
    }
}
