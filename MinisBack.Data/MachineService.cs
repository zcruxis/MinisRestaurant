using MinisBack.Data.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MinisBack.Data
{
    public class MachineService
    {
        public bool Full { get; set; }
        [MaxLength(5)]
        public int SandwichesNumber { get; set; }
        public int WorkingOrderId { get; set; }

        public void ProcessInbounds(string Command,int OrderItemId,int OrderId,int itemNumber)
        {
            /* To be implemented
            this.SandwichesNumber++;
            if (this.SandwichesNumber == 5) this.Full = true;
            else                            this.Full = false;
            
            //if(Full) Queue += { string Command,int OrderItemId,int OrderId,int itemNumber};
            */
            string[] stringSeparators = new string[] { "+" };
            var result = Command.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(result);
            var Size = result.Count();
            int Counter = 0;
            bool First = false;
            bool Last = false;
            foreach (var cmd in result)
            {
                if (Counter == 0)         First = true;
                else if (Counter == Size) Last = true;
                else {
                         First = false;
                         Last = false;
                     }

                if (cmd == "addb()")        AddBread(OrderItemId,First,Last);
                if (cmd == "addS()")        AddSalsa(OrderItemId,Last);
                if (cmd == "Compress()")    CompressSandwich(OrderItemId);
                if (cmd.Contains("addIng"))
                {
                    var Ingredients = "";
                    Ingredients = cmd.Replace("AddIng(", "");
                    Ingredients = cmd.Replace(")", "");
                    AddIngredients(Ingredients, OrderItemId);
                }
                if (cmd.Contains("Cut"))
                {
                    var Cut = "";
                    Cut = cmd.Replace("Cut(", "");
                    Cut = cmd.Replace(")", "");
                    CutSandwich(Cut, OrderItemId);
                }
            }
            //To be implemented
            //this.SandwichesNumber--;
            //To Complete order
            //if(Counter = itemNumber)
            // Query needed to set the Complete Order
            //   var QComplete = UPDATE from Orders in context.orders values(Order.Completed = true) where OrderId == Order.Id;
        }

        private void CutSandwich(string cut, int OrderItemId)
        {
           
        }

        private void CompressSandwich(int OrderItemId)
        {
            // Queries needed to set the Contents
            //   var QCompress = UPDATE from OrderItems in context.orderItems values(OrderItem.Compressed = true) where OrderItem.Id == OrderItemId;
        }

        private void AddIngredients(string ingredients, int OrderItemId)
        {
            var Contents = "";
            string[] stringSeparators = new string[] { "," };
            var result = ingredients.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

            // Queries needed to set the Contents
            //var QIngredients = from OrderItems in context.OrderItems where OrderItem.Id == OrderItemId select context.OrderItems.Contents;
            // Contents = QIngredients.Contents;

            foreach (var ing in result)
            {
                 Contents += ing;
            }

            //var QCut = UPDATE from OrderItems in context.orderItems values(OrderItem.Contents) where OrderItem.Id == OrderItemId;
            //
        }

        private void AddSalsa(int OrderItemId, bool last)
        {
            var Contents = "";
            // Queries needed to set the Contents
            //var QIngredients = from OrderItems in context.OrderItems where OrderItem.Id == OrderItemId select context.OrderItems.Contents;
            // Contents = QIngredients.Contents;
            if (last) Contents += ",Salsa]";
            else      Contents += ",Salsa"; ;
           
            //var QCut = UPDATE from OrderItems in context.orderItems values(OrderItem.Contents) where OrderItem.Id == OrderItemId;
            
        }

        private void AddBread(int OrderItemId,bool first, bool last)
        {
            var Contents = "";
            // Queries needed to set the Contents
            //var QIngredients = from OrderItems in context.OrderItems where OrderItem.Id == OrderItemId select context.OrderItems.Contents;
            // Contents = QIngredients.Contents;
            if (first) Contents += "[Bread";
            else if (last) Contents += ",Bread]";
            else Contents += ",Bread";
            //var QCut = UPDATE from OrderItems in context.orderItems values(OrderItem.Contents) where OrderItem.Id == OrderItemId;
        }
    }
}

