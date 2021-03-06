﻿// -----------------------------------------------------------------------
// <copyright company="Fireasy"
//      email="faib920@126.com"
//      qq="55570729">
//   (c) Copyright Fireasy. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Reflection;

namespace Fireasy.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TreeListFooterItem : TreeListItem
    {
        private object statInfo;

        internal TreeListFooterItem(TreeListItem item)
        {
            Text = item.Text;

            foreach (var cell in item.Cells)
            {
                Cells.Add(cell);
            }
        }

        public TreeListFooterItem(object statInfo)
        {
            this.statInfo = statInfo;
        }

        internal override void Update(TreeList treelist, TreeListItem parent, int level)
        {
            if (statInfo != null)
            {
                for (var i = Cells.Count; i < treelist.Columns.Count; i++)
                {
                    var cell = new TreeListCell();
                    cell.Value = GetStatPropertyValue(treelist.Columns[i].DataKey);

                    Cells.Add(cell);
                }
            }

            base.Update(treelist, parent, level);
        }

        private object GetStatPropertyValue(string dataKey)
        {
            if (statInfo != null && !string.IsNullOrEmpty(dataKey))
            {
                var p = statInfo.GetType().GetProperty(dataKey, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null && p.CanRead)
                {
                    return p.GetValue(statInfo, null);
                }
            }

            return null;
        }
    }
}
