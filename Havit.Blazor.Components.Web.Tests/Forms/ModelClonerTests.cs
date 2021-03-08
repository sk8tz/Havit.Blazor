﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace Havit.Blazor.Components.Web.Tests
{
	[TestClass]
	public class ModelClonerTests
	{
		[TestMethod]
		public void ModelCloner_TryCloneClonable()
		{
			// Arrange
			Mock<ICloneable> modelMock = new Mock<ICloneable>(MockBehavior.Strict);
			modelMock.Setup(m => m.Clone()).Returns(() => modelMock.Object); // we do not need true clone (in this test).

			// Act
			bool success = ModelCloner.TryCloneClonable(modelMock.Object, out ICloneable modelClone);

			// Assert
			Assert.IsTrue(success);
			modelMock.Verify(m => m.Clone(), Times.Once()); // Clone was called
		}


		[TestMethod]
		public void ModelCloner_TryCloneRecord()
		{
			// Arrange
			ModelRecord model = new ModelRecord { Name = "Mr. Unit Test" };

			// Act
			bool success = ModelCloner.TryCloneRecord(model, out ModelRecord modelClone);

			// Assert
			Assert.IsTrue(success);
			Assert.AreNotSame(model, modelClone);
			Assert.AreEqual(model.Name, modelClone.Name);
		}

		[TestMethod]
		public void ModelCloner_CloneMemberwiseClone()
		{
			// Arrange
			ModelClass model = new ModelClass { Name = "Mr. Unit Test" };

			// Act
			ModelClass modelClone = ModelCloner.CloneMemberwiseClone(model);

			// Assert
			Assert.AreNotSame(model, modelClone);
			Assert.AreEqual(model.Name, modelClone.Name);
		}

		public record ModelRecord
		{
			public string Name { get; set; }
		}

		public class ModelClass
		{
			public string Name { get; set; }
		}
	}
}
