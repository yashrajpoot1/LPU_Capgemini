using System;
public class Desktop : Computer
{
  public int MonitorSize { get; set; }
  public int PowerSupplyVolt { get; set; }
  public double DesktopPriceCalculation()
  {
    double processorCost = 0;
    switch (Processor)
    {
      case "i3":
        processorCost = 1500;
        break;
      case "i5":
        processorCost = 3000;
        break;
      case "i7":
        processorCost = 4500;
        break;
    }
    double totalPrice = processorCost + (RamSize * 200) + (HardDiskSize * 1500) + (GraphicCard * 2500) + (MonitorSize * 250) + (PowerSupplyVolt * 20);
    return totalPrice;
  }
}

