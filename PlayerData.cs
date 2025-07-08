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

    public List<Item?> EquipItem { get; set; } // 착용 중인 아이템 목록 (무기, 방어구 등)

    public List<Item?> Items { get; set; }
    public List<Skill?> Skills { get; set; }
}

