using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.enums
{
    public enum PalletQuantityEnum
    {
        Mobile = 20,
        Laptop = 10,
        HeadPhone = 30,
        Mouse = 30
    }

    public enum ProductTypeIds
    {
        Mobile = 1,
        Laptop = 2,
        HeadPhone = 3,
        Mouse = 4
    }

    public enum NodesIds
    {
        Node1 = 1,
        Node2 = 2,
        Node3 = 3
    }
    //Provide discount if quantity of these products order is > than the mentioned below
    public enum DiscountQuantity
    {
        Mobile = 3,
        Laptop = 2,
        HeadPhone = 3,
        Mouse = 4
    }
    public enum DiscountPercentage
    {
        Mobile = 5,
        Laptop = 5,
        HeadPhone = 5,
        Mouse = 5
    }
}
