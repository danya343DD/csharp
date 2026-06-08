using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace nagaev
{
    [Serializable]
    public class PaymentInvoice : ISerializable
    {
        // Основные поля
        private decimal dailyRate;
        private int days;
        private decimal finePerDay;
        private int delayDays;

        // Вычисляемые поля (хранятся для демонстрации сериализации/десериализации)
        private decimal amountWithoutFine;
        private decimal fine;
        private decimal totalAmount;

        // Статическое свойство для управления режимом сериализации
        public static bool SerializeAllFields { get; set; } = true;

        public decimal DailyRate { get { return dailyRate; } set { dailyRate = value; Recalculate(); } }
        public int Days { get { return days; } set { days = value; Recalculate(); } }
        public decimal FinePerDay { get { return finePerDay; } set { finePerDay = value; Recalculate(); } }
        public int DelayDays { get { return delayDays; } set { delayDays = value; Recalculate(); } }

        public decimal AmountWithoutFine { get { return amountWithoutFine; } }
        public decimal Fine { get { return fine; } }
        public decimal TotalAmount { get { return totalAmount; } }

        public PaymentInvoice(decimal dailyRate, int days, decimal finePerDay, int delayDays)
        {
            this.dailyRate = dailyRate;
            this.days = days;
            this.finePerDay = finePerDay;
            this.delayDays = delayDays;
            Recalculate();
        }

        private void Recalculate()
        {
            amountWithoutFine = dailyRate * days;
            fine = finePerDay * delayDays;
            totalAmount = amountWithoutFine + fine;
        }

        // Конструктор десериализации
        protected PaymentInvoice(SerializationInfo info, StreamingContext context)
        {
            dailyRate = info.GetDecimal("dailyRate");
            days = info.GetInt32("days");
            finePerDay = info.GetDecimal("finePerDay");
            delayDays = info.GetInt32("delayDays");

            // Проверяем, были ли сохранены вычисляемые поля
            bool fieldsExist = false;
            foreach (SerializationEntry entry in info)
            {
                if (entry.Name == "totalAmount")
                {
                    fieldsExist = true;
                    break;
                }
            }

            if (fieldsExist)
            {
                amountWithoutFine = info.GetDecimal("amountWithoutFine");
                fine = info.GetDecimal("fine");
                totalAmount = info.GetDecimal("totalAmount");
            }
            else
            {
                // Если не сериализовались — вычисляем заново
                Recalculate();
            }
        }

        // Метод сериализации
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Базовые поля сериализуются всегда
            info.AddValue("dailyRate", dailyRate);
            info.AddValue("days", days);
            info.AddValue("finePerDay", finePerDay);
            info.AddValue("delayDays", delayDays);

            // Вычисляемые поля сериализуются только если статический флаг равен true
            if (SerializeAllFields)
            {
                info.AddValue("amountWithoutFine", amountWithoutFine);
                info.AddValue("fine", fine);
                info.AddValue("totalAmount", totalAmount);
            }
        }

        public override string ToString()
        {
            return $"Оплата/день: {dailyRate} | Дней: {days} | Штраф/день: {finePerDay} | Задержка (дней): {delayDays}\n" +
                   $"Сумма без штрафа: {amountWithoutFine} | Штраф: {fine} | ИТОГО К ОПЛАТЕ: {totalAmount}\n";
        }
    }

    class Program
    {
        static void Main()
        {
            string filePath = "invoice.dat";
            BinaryFormatter formatter = new BinaryFormatter();
            // Создаем тестовый счет
            PaymentInvoice originalInvoice = new PaymentInvoice(150, 10, 25, 4);

            //ТЕСТ 1: Сериализация ВСЕХ полей (true)
            PaymentInvoice.SerializeAllFields = true;
            Console.WriteLine("ТЕСТ 1: Сериализация со всеми полями");
            Console.WriteLine("Исходный объект:\n" + originalInvoice);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fs, originalInvoice);
            }

            PaymentInvoice deserializedInvoice1;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                deserializedInvoice1 = (PaymentInvoice)formatter.Deserialize(fs);
            }
            Console.WriteLine("Восстановленный объект:\n" + deserializedInvoice1);

            //ТЕСТ 2: Сериализация БЕЗ вычисляемых полей (false)
            PaymentInvoice.SerializeAllFields = false;
            Console.WriteLine("=== ТЕСТ 2: Сериализация БЕЗ вычисляемых полей ===");

            // Намеренно исказим внутренние вычисления перед сохранением, чтобы доказать, 
            // что при false они не сохраняются, а будут пересчитаны с нуля в конструкторе
            PaymentInvoice invoiceToSave = new PaymentInvoice(200, 5, 50, 2);
            Console.WriteLine("Исходный объект:\n" + invoiceToSave);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fs, invoiceToSave);
            }

            PaymentInvoice deserializedInvoice2;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                deserializedInvoice2 = (PaymentInvoice)formatter.Deserialize(fs);
            }
            Console.WriteLine("Восстановленный объект (вычисляемые поля пересчитаны заново):\n" + deserializedInvoice2);

            Console.ReadLine();
        }
    }
};
