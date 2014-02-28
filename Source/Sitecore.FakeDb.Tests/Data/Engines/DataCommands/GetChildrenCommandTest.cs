﻿namespace Sitecore.FakeDb.Tests.Data.Engines.DataCommands
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Collections;
  using Sitecore.Data.Engines;
  using Sitecore.FakeDb.Data.Engines;
  using Sitecore.FakeDb.Data.Engines.DataCommands;
  using Sitecore.FakeDb.Data.Items;
  using Xunit;

  public class GetChildrenCommandTest : CommandTestBase
  {
    [Fact]
    public void ShouldCreateInstance()
    {
      // arrange
      var command = new OpenGetChildrenCommand(this.dataStorage);

      // act & assert
      command.CreateInstance().Should().BeOfType<GetChildrenCommand>();
    }

    [Fact]
    public void ShouldReturnItemChildren()
    {
      // arrange
      var dbchild1 = new DbItem("child1");
      var dbchild2 = new DbItem("child2");
      var dbitem = new DbItem("item") { dbchild1, dbchild2 };

      var child1 = ItemHelper.CreateInstance(this.database);
      var child2 = ItemHelper.CreateInstance(this.database);
      var item = ItemHelper.CreateInstance(dbitem.ID, this.database);

      this.dataStorage.GetFakeItem(dbitem.ID).Returns(dbitem);
      this.dataStorage.GetSitecoreItem(dbchild1.ID, item.Language).Returns(child1);
      this.dataStorage.GetSitecoreItem(dbchild2.ID, item.Language).Returns(child2);

      var command = new OpenGetChildrenCommand(this.dataStorage) { Engine = new DataEngine(this.database) };
      command.Initialize(item);

      // act
      var children = command.DoExecute();

      // assert
      children[0].Should().Be(child1);
      children[1].Should().Be(child2);
    }

    private class OpenGetChildrenCommand : GetChildrenCommand
    {
      public OpenGetChildrenCommand(DataStorage dataStorage)
        : base(dataStorage)
      {
      }

      public new Sitecore.Data.Engines.DataCommands.GetChildrenCommand CreateInstance()
      {
        return base.CreateInstance();
      }

      public new ItemList DoExecute()
      {
        return base.DoExecute();
      }
    }
  }
}