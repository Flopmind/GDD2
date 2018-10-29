using UnityEditor;
using System;
using System.Collections.Generic;
// https://gist.github.com/michaelbartnett/5652076
/// <summary>
/// Represents a functional tuple that can be used to store
/// two values of different types inside one object.
/// </summary>
/// <typeparam name="T1">The type of the first element</typeparam>
/// <typeparam name="T2">The type of the second element</typeparam>
/// <typeparam name="T3">The type of the third element</typeparam>
public sealed class Tuple<T1, T2, T3>
    {
        private readonly T1 item1;
        private readonly T2 item2;
        private readonly T3 item3;

        /// <summary>
        /// Retyurns the first element of the tuple
        /// </summary>
        public T1 Item1
        {
            get { return item1; }
        }

        /// <summary>
        /// Returns the second element of the tuple
        /// </summary>
        public T2 Item2
        {
            get { return item2; }
        }

        /// <summary>
        /// Returns the second element of the tuple
        /// </summary>
        public T3 Item3
        {
            get { return item3; }
        }

        /// <summary>
        /// Create a new tuple value
        /// </summary>
        /// <param name="item1">First element of the tuple</param>
        /// <param name="second">Second element of the tuple</param>
        /// <param name="third">Third element of the tuple</param>
        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (item1 == null ? 0 : item1.GetHashCode());
            hash = hash * 23 + (item2 == null ? 0 : item2.GetHashCode());
            hash = hash * 23 + (item3 == null ? 0 : item3.GetHashCode());
            return hash;
        }

        public override bool Equals(object o)
        {
            if (!(o is Tuple<T1, T2, T3>)) {
                return false;
            }

            var other = (Tuple<T1, T2, T3>)o;

            return this == other;
        }

        public static bool operator==(Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
        {
            if (object.ReferenceEquals(a, null)) {
                return object.ReferenceEquals(b, null);
            }
            if (a.item1 == null && b.item1 != null) return false;
            if (a.item2 == null && b.item2 != null) return false;
            if (a.item3 == null && b.item3 != null) return false;
            return
                a.item1.Equals(b.item1) &&
                a.item2.Equals(b.item2) &&
                a.item3.Equals(b.item3);
        }

        public static bool operator!=(Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
        {
            return !(a == b);
        }

        public void Unpack(Action<T1, T2, T3> unpackerDelegate)
        {
            unpackerDelegate(Item1, Item2, Item3);
        }
}public class BuildCommand {
	private static Dictionary<string,Tuple<BuildTargetGroup,BuildTarget,string>> types=new Dictionary<string, Tuple<BuildTargetGroup, BuildTarget, string>>(){
		{"linux32", new Tuple<BuildTargetGroup, BuildTarget, string>(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux, "")},
		{"linux64", new Tuple<BuildTargetGroup, BuildTarget, string>(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64, "")},
		{"linux", new Tuple<BuildTargetGroup, BuildTarget, string>(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinuxUniversal, "")},
		{"osx", new Tuple<BuildTargetGroup, BuildTarget, string>(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX, ".dmg")},
		{"win32", new Tuple<BuildTargetGroup, BuildTarget, string>(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows, ".exe")},
		{"win64", new Tuple<BuildTargetGroup, BuildTarget, string>(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, ".exe")},
		{"web", new Tuple<BuildTargetGroup, BuildTarget, string>(BuildTargetGroup.WebGL, BuildTarget.WebGL, "")}
	};
	private static BuildPlayerOptions GetBuildPlayerOptions(string platform) {
		Console.WriteLine("Building for: "+platform);
		return new BuildPlayerOptions(){
			scenes=new string[]{"Assets/Scenes/SampleScene.unity"},
			locationPathName="Build/"+platform+"/GDDGame"+types[platform].Item3,
			targetGroup=types[platform].Item1,
			target=types[platform].Item2
		};
	}
	static void PerformBuild() {
		string[] args=System.Environment.GetCommandLineArgs();
		string buildFor=null;
		for(int i=0;i<args.Length-1;i++)
		{
			if(args[i]=="-buildFor"){
				buildFor=args[i+1].ToLower();
			}
		}
		if(buildFor==null) {
			System.Environment.Exit(1);
		}
		BuildPlayerOptions options=GetBuildPlayerOptions(buildFor);
        BuildPipeline.BuildPlayer(options);
	}
}
