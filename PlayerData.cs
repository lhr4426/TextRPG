using System;

using System.Text.Json;
public class PlayerData
{
    public string Name { get; set; }
    public int Level { get; set; }
    public string Job { get; set; }
    public int ATK { get; set; }
    public int DEF { get; set; }
    public int HP { get; set; }
    public int Gold { get; set; }

    public List<IItem> Items { get; set; }
    public List<ISkill> Skills { get; set; }
}

