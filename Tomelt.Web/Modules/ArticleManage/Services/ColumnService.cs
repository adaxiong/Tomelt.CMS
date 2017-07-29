using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ArticleManage.Models;
using ArticleManage.ViewModels;
using Tomelt;
using Tomelt.ContentManagement;
using Tomelt.Core.Common.Models;
using Tomelt.Core.Title.Models;
using Tomelt.Data;

namespace ArticleManage.Services
{
    public class ColumnService : IColumnService
    {
        public IRepository<ColumnPartRecord> ColumnRepository { get; set; }
        public ITomeltServices TomeltServices { get; set; }
        public ColumnService(ITomeltServices tomeltServices, IRepository<ColumnPartRecord> columnRepository)
        {
            TomeltServices = tomeltServices;
            ColumnRepository = columnRepository;
        }
        public IContentQuery<ContentItem> GetColumns(VersionOptions versionOptions)
        {
            return TomeltServices.ContentManager.Query(versionOptions, "Column");

        }
        public List<EasyuiTree> GetTreeColumns(VersionOptions versionOptions)
        {
            return GetTree(0, GetColumns(versionOptions).OrderBy<ColumnPartRecord>(d=>d.Sort).List());

        }

        public void UpdateForContentItem(ContentItem item, EditColumnPartViewModel viewModel)
        {
            var part = item.As<ColumnPart>();
            part.CallIndex = viewModel.CallIndex;
            part.Groups = viewModel.Groups;
            part.Sort = viewModel.Sort;
            part.ImageUrl = viewModel.ImageUrl;
            part.LinkUrl = viewModel.LinkUrl;
            part.Summary = viewModel.Summary;
            part.ParentId = viewModel.ParentId;
            if (IsContainNode(part.Id, part.ParentId))
            {
                var oldEntity = TomeltServices.ContentManager.Get<ColumnPart>(part.Id, VersionOptions.Latest);
                var treePath = "," + part.ParentId + ",";
                var layer = 1;
                if (oldEntity.ParentId > 0)
                {
                    var oldParentEntity = TomeltServices.ContentManager.Get<ColumnPart>(oldEntity.ParentId, VersionOptions.Latest);
                    treePath = oldParentEntity.TreePath + part.ParentId + ",";
                    layer = oldParentEntity.Layer + 1;

                }
                var temp = TomeltServices.ContentManager.Get<ColumnPart>(part.ParentId, VersionOptions.Latest);
                temp.TreePath = treePath;
                temp.Layer = layer;
                temp.ParentId = oldEntity.ParentId;
                UpdateChilds(part.ParentId);
            }
            if (part.ParentId > 0)
            {
                var entity = TomeltServices.ContentManager.Get<ColumnPart>(part.ParentId,VersionOptions.Latest);
                part.TreePath = entity.TreePath + part.Id + ",";
                part.Layer = entity.Layer + 1;
            }
            else
            {
                part.TreePath = "," + part.Id + ",";
                part.Layer = 1;
            }
            var newEntity = TomeltServices.ContentManager.Get<ColumnPart>(part.Id, VersionOptions.Latest);
            newEntity.TreePath = part.TreePath;
            newEntity.Layer = part.Layer;
            UpdateChilds(part.Id);
            ColumnRepository.Flush();
        }

        /// <summary>
        /// 验证节点是否被包含
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private bool IsContainNode(int id, int parentId)
        {
            var query = TomeltServices.ContentManager.Query<ColumnPart, ColumnPartRecord>(VersionOptions.Latest)
                .Where(d => d.TreePath.Contains("," + id + ",") && d.Id == parentId);

            return query.Count() > 0;
        }

        private void UpdateChilds(int parentId)
        {
            var entity = TomeltServices.ContentManager.Get<ColumnPart>(parentId, VersionOptions.Latest);
            if (entity != null)
            {
                var list = TomeltServices.ContentManager.Query<ColumnPart, ColumnPartRecord>(VersionOptions.Latest)
                    .Where(d => d.ParentId == parentId).List();
                foreach (var treePartRecord in list)
                {
                    string treePath = entity.TreePath + treePartRecord.Id + ",";
                    int layer = entity.Layer + 1;
                    treePartRecord.TreePath = treePath;
                    treePartRecord.Layer = layer;
                    UpdateChilds(treePartRecord.Id);
                }
            }
        }
        private List<EasyuiTree> GetTree(int parentId, IEnumerable<ContentItem> list)
        {
            var newlist = list.Where(d => d.As<ColumnPart>().ParentId == parentId).ToList();
            List<EasyuiTree> jsonData = new List<EasyuiTree>();
            newlist.ForEach(item =>
            {
                jsonData.Add(new EasyuiTree
                {
                    id = item.Id,
                    children = GetTree(item.Id, list),
                    ParentId = item.As<ColumnPart>().ParentId,
                    text = item.As<TitlePart>().Title
                });
            });
            return jsonData;
        }
    }
}