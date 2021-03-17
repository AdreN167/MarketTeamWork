

using System;
using Qiwi.BillPayments;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model.Out;
using Qiwi.BillPayments.Utils;
using System.Diagnostics;
using System.Threading;

namespace Market.PaymentMethods
{
    public class QIWI : IPayment
    {
        public bool ToPay(decimal MoneyAmount)
        {

            //MoneyAmount заменяется на 1 тенге
            MoneyAmount = 1;

            //Секретный ключ отправителя счета
            const string ownerSecretKey = "eyJ2ZXJzaW9uIjoiUDJQIiwiZGF0YSI6eyJwYXlpbl9tZXJjaGFudF9zaXRlX3VpZCI6InNkbmNjaC0wMCIsInVzZXJfaWQiOiI3NzAxMzk4OTM5NiIsInNlY3JldCI6ImVjMDc5Y2RlNmYwNTMzZTMyNDM1MzFkMDQ0ZmJhNzM2NTc0ZWI2NWZkNDUzZmUzYWViNTFmMzUzNzI1MjNhNDQifX0=";
            const int second = 1000; //Одна секунда равняется 1000 мл.секунд
            const int updateTime = 10; //Время для обновления статуса оплаты
            const int numberDaysForPayment = 5; //Количество дней для оплаты счета
            string currentStatus; //Текущий статус оплаты
            int numberLoops = 15;

            BillPaymentsClient qiwiClient = BillPaymentsClientFactory.Create(secretKey: ownerSecretKey);


            BillResponse qiwiResponse = qiwiClient.CreateBill(
                info: new CreateBillInfo
                {
                    BillId = Guid.NewGuid().ToString(),
                    Amount = new MoneyAmount
                    {
                        ValueDecimal = MoneyAmount,
                        CurrencyEnum = CurrencyEnum.Kzt
                    },
                    ExpirationDateTime = DateTime.Now.AddDays(numberDaysForPayment),
                }
            );

            BillResponse response = qiwiClient.GetBillInfo(billId: qiwiResponse.BillId);

            //Открыть URL в браузере
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = response.PayUrl.AbsoluteUri;
            process.Start();


            int counter = new int();
            while (true)
            {
                if (counter == numberLoops)
                    return false;

                try
                {
                    currentStatus = qiwiClient.GetBillInfo(billId: qiwiResponse.BillId).Status.ValueString;
                }
                catch
                {
                    currentStatus = "WAITING";
                }
                Thread.Sleep(second * updateTime);
                Console.Clear();

                switch (currentStatus)
                {
                    case "PAID":
                        return true;

                    case "REJECTED":
                        return false;

                    case "WAITING":
                        continue;

                }

                counter++;

            }
        }
    }
}
