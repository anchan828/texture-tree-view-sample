using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

public class TextureTableHeader : MultiColumnHeader
{
    public TextureTableHeader(MultiColumnHeaderState state) : base(state)
    {
        
//      カラムを追加する順番が、そのまま表示する順番となっている。
//      そのため、いつでもColumnIndexの変更でカラムの順番を入れ替えができるように、次の方法で追加している。
        var columns = new List<MultiColumnHeaderState.Column>();

        foreach (ColumnIndex value in Enum.GetValues(typeof(ColumnIndex)))
        {
            switch (value)
            {
                case ColumnIndex.Id:
                    columns.Add(new IdColumn());
                    break;
                case ColumnIndex.Icon:
                    columns.Add(new IconColumn());
                    break;
                case ColumnIndex.Name:
                    columns.Add(new NameColumn());
                    break;
                case ColumnIndex.Extension:
                    columns.Add(new ExtensionColumn());
                    break;
                case ColumnIndex.Bytes:
                    columns.Add(new BytesColumn());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

//        カラムを追加する順番を守ればよいで、単純に追加するなら次の方法で問題ない。
//        var columns = new MultiColumnHeaderState.Column[]
//        {
//            new IdColumn(),
//            new IconColumn(),
//            new NameColumn(),
//            new ExtensionColumn(),
//            new BytesColumn()
//        };    
        this.state = new MultiColumnHeaderState(columns.ToArray());
    }
}