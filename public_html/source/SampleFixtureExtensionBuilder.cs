using System;
using NUnit.Core.Builders;
using NUnit.Core.Extensibility;

namespace NUnit.Core.Extensions
{
	/// <summary>
	/// MockFixtureExtensionBuilder knows how to build
	/// a MockFixtureExtension.
	/// </summary>
	[NUnitAddin(Description="Wraps an NUnitTestFixture with an additional level of SetUp and TearDown")]
	public class SampleFixtureExtensionBuilder : NUnitTestFixtureBuilder, IAddin
	{	
		#region NUnitTestFixtureBuilder Overrides
		/// <summary>
		/// Makes a SampleFixtureExtension instance
		/// </summary>
		/// <param name="type">The type to be used</param>
		/// <returns>A SampleFixtueExtension as a TestSuite</returns>
		protected override TestSuite MakeSuite(Type type)
		{
			return new SampleFixtureExtension( type );
		}

		// The builder recognizes the types that it can use by the presense
		// of SampleFixtureExtensionAttribute. Note that an attribute does not
		// have to be used. You can use any arbitrary set of rules that can be 
		// implemented using reflection on the type.
		public override bool CanBuildFrom(Type type)
		{
			return Reflect.HasAttribute( type, "NUnit.Core.Extensions.SampleFixtureExtensionAttribute", false );
		}
		#endregion

		#region IAddin Members
		public bool Install(IExtensionHost host)
		{
			IExtensionPoint suiteBuilders = host.GetExtensionPoint( "SuiteBuilders" );
			if ( suiteBuilders == null )
				return false;

			suiteBuilders.Install( this );
			return true;
		}
		#endregion
	}
}
