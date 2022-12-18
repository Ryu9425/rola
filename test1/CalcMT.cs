using System.Collections.Generic;
using System.Linq;

using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics;


namespace ActuatorInspectionCommon
{
    /// <summary>
    /// MT距離の計算
    /// </summary>
    public class CalcMT
    {
        private void TestMathNet()
        {
            //TestGyakuGyoretsu();
            //TestMT1();
            //TestGyakuGyoretsu_honban();
            //TestMT3num();
            //TestMT4num();
            TestMT2();
        }

        private void TestGyakuGyoretsu()
        {
            // こんな風に行列を初期化できる
            var M1 = DenseMatrix.OfArray(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 9, 8 } });
            // 逆行列
            var M2 = M1.Inverse();

            System.Diagnostics.Debug.WriteLine("元の行列 M1");
            System.Diagnostics.Debug.WriteLine(M1);
            System.Diagnostics.Debug.WriteLine("M1 の逆行列");
            System.Diagnostics.Debug.WriteLine(M2);
            System.Diagnostics.Debug.WriteLine("M2 * M1 は単位行列 I になっているはず!");
            System.Diagnostics.Debug.WriteLine(M2 * M1);
        }

        /// <summary>
        /// MT法テスト1
        /// </summary>
        private void TestMT1()
        {
            var data = new double[] { 3, 4, 5, 6, 10 };
            // Listとかでも大丈夫
            // var data = new List<double>() { 3, 4, 5, 6, 10 };

            // 拡張メソッドで簡単に使える
            System.Diagnostics.Debug.WriteLine("平均　　　：{0}", data.Mean());
            System.Diagnostics.Debug.WriteLine("中央値　　：{0}", data.Median());
            System.Diagnostics.Debug.WriteLine("分散　　　：{0}", data.PopulationVariance());
            System.Diagnostics.Debug.WriteLine("母分散　　：{0}", data.Variance());
            System.Diagnostics.Debug.WriteLine("標準偏差　：{0}", data.PopulationStandardDeviation());
            System.Diagnostics.Debug.WriteLine("母標準偏差：{0}", data.StandardDeviation());
        }

        private static DenseMatrix GetTestData3num()
        {
            return DenseMatrix.OfArray(new double[,]
            {
                {441,8,0.2 },
                {441,8,0.1 },
                {450,7,0.1 },
                {450,6,0.2 },
                {450,8,0.3 },
                {450,7,0.3 },
                {441,7,0.2 },
                {441,6,0.1 },
                {450,7,0.1 },
                {441,6,0.2 },
                {441,7,0.3 },
                {441,7,0.3 },
                {441,6,0.2 },
                {450,7,0.1 },
                {450,7,0.1 },
                {441,6,0.2 },
                {450,6,0.3 },
                {441,7,0.3 },
                {441,6,0.2 },
                {450,7,0.1 },
                {441,8,0.1 },
                {450,7,0.2 },
                {450,7,0.3 },
                {450,7,0.3 },
                {441,7,0.2 },
                {450,7,0.1 },
                {441,8,0.1 },
                {450,7,0.2 },
                {450,8,0.3 },
                {450,8,0.3 },
                {450,6,0.2 },
                {441,7,0.1 },
                {450,7,0.1 },
                {441,6,0.2 },
                {441,7,0.3 },
                {450,8,0.3 },
                {450,7,0.2 },
                {441,7,0.1 },
                {450,7,0.1 },
                {441,6,0.2 },
                {450,7,0.3 },
                {441,7,0.3 },
                {441,7,0.2 },
                {450,6,0.1 },
                {450,8,0.1 },
                {441,5,0.2 },
                {450,7,0.3 },
                {441,7,0.3 },
                {450,9,0.2 },
                {450,7,0.1 },
                {441,8,0.1 },
                {450,7,0.2 },
                {441,8,0.3 },
                {450,8,0.3 },
                {441,7,0.2 },
                {450,8,0.1 },
                {450,7,0.1 },
                {441,7,0.2 },
                {441,7,0.3 },
                {450,8,0.3 }
            });
        }

        private static DenseMatrix GetTestData4num()
        {
            return DenseMatrix.OfArray(new double[,]
            {
                {441,8,0.1 ,0.2 },
                {441,8,0.1 ,0.1 },
                {450,7,0.1 ,0.1 },
                {450,6,0.1 ,0.2 },
                {450,8,0.1 ,0.3 },
                {450,7,0.1 ,0.3 },
                {441,7,0.1 ,0.2 },
                {441,6,0.1 ,0.1 },
                {450,7,0.1 ,0.1 },
                {441,6,0.1 ,0.2 },
                {441,7,0.1 ,0.3 },
                {441,7,0.1 ,0.3 },
                {441,6,0.1 ,0.2 },
                {450,7,0.1 ,0.1 },
                {450,7,0.1 ,0.1 },
                {441,6,0.1 ,0.2 },
                {450,6,0.1 ,0.3 },
                {441,7,0.1 ,0.3 },
                {441,6,0.1 ,0.2 },
                {450,7,0.1 ,0.1 },
                {441,8,0.1 ,0.1 },
                {450,7,0.1 ,0.2 },
                {450,7,0.1 ,0.3 },
                {450,7,0.1 ,0.3 },
                {441,7,0.1 ,0.2 },
                {450,7,0.1 ,0.1 },
                {441,8,0.1 ,0.1 },
                {450,7,0.1 ,0.2 },
                {450,8,0.1 ,0.3 },
                {450,8,0.1 ,0.3 },
                {450,6,0.1 ,0.2 },
                {441,7,0.1 ,0.1 },
                {450,7,0.1 ,0.1 },
                {441,6,0.1 ,0.2 },
                {441,7,0.1 ,0.3 },
                {450,8,0.1 ,0.3 },
                {450,7,0.1 ,0.2 },
                {441,7,0.1 ,0.1 },
                {450,7,0.1 ,0.1 },
                {441,6,0.1 ,0.2 },
                {450,7,0.1 ,0.3 },
                {441,7,0.1 ,0.3 },
                {441,7,0.1 ,0.2 },
                {450,6,0.1 ,0.1 },
                {450,8,0.1 ,0.1 },
                {441,5,0.1 ,0.2 },
                {450,7,0.2 ,0.3 },
                {441,7,0.1 ,0.3 },
                {450,9,0.1 ,0.2 },
                {450,7,0.1 ,0.1 },
                {441,8,0.1 ,0.1 },
                {450,7,0.1 ,0.2 },
                {441,8,0.1 ,0.3 },
                {450,8,0.1 ,0.3 },
                {441,7,0.1 ,0.2 },
                {450,8,0.1 ,0.1 },
                {450,7,0.1 ,0.1 },
                {441,7,0.1 ,0.2 },
                {441,7,0.1 ,0.3 },
                {450,8,0.2 ,0.3 }

            });
        }

        /// <summary>
        /// MT法テスト3項目
        /// </summary>
        private void TestMT3num()
        {
            var data = GetTestData3num();

            var cols = data.ToColumnArrays();

            // 列ごとの平均を求める
            var heikin = new double[]{
                                cols[0].Mean(),
                                cols[1].Mean(),
                                cols[2].Mean() };

            // 各要素から平均をひく
            for (int x = 0; x < data.ColumnCount; x++)
            {
                for (int y = 0; y < data.RowCount; y++)
                {
                    data[y, x] -= heikin[x];
                }
            }

            // 列ごとの標準偏差を求める
            var hyojunHensa = new double[]{
                                cols[0].PopulationStandardDeviation(),
                                cols[1].PopulationStandardDeviation(),
                                cols[2].PopulationStandardDeviation() };
            // 各要素を標準偏差で割る
            for (int x = 0; x < data.ColumnCount; x++)
            {
                for (int y = 0; y < data.RowCount; y++)
                {
                    data[y, x] /= hyojunHensa[x];
                }
            }

            // 相関係数を求める= 各要素を基準化((値 - 平均) / 標準偏差)したものに列
            var sokanKeisu = DenseMatrix.Create(3, 3, 0);
            for (int y = 0; y < data.RowCount; y++)
            {
                // γ11
                sokanKeisu[0, 0] += data[y, 0] * data[y, 0];
                // γ12
                sokanKeisu[1, 0] += data[y, 0] * data[y, 1];
                // γ13
                sokanKeisu[2, 0] += data[y, 0] * data[y, 2];
                // γ21
                sokanKeisu[0, 1] += data[y, 1] * data[y, 0];
                // γ22
                sokanKeisu[1, 1] += data[y, 1] * data[y, 1];
                // γ23
                sokanKeisu[2, 1] += data[y, 1] * data[y, 2];
                // γ31
                sokanKeisu[0, 2] += data[y, 2] * data[y, 0];
                // γ32
                sokanKeisu[1, 2] += data[y, 2] * data[y, 1];
                // γ32
                sokanKeisu[2, 2] += data[y, 2] * data[y, 2];
            }
            // γ11
            sokanKeisu[0, 0] /= data.RowCount;
            // γ12
            sokanKeisu[1, 0] /= data.RowCount;
            // γ13
            sokanKeisu[2, 0] /= data.RowCount;
            // γ21
            sokanKeisu[0, 1] /= data.RowCount;
            // γ22
            sokanKeisu[1, 1] /= data.RowCount;
            // γ23
            sokanKeisu[2, 1] /= data.RowCount;
            // γ31
            sokanKeisu[0, 2] /= data.RowCount;
            // γ32
            sokanKeisu[1, 2] /= data.RowCount;
            // γ33
            sokanKeisu[2, 2] /= data.RowCount;

            System.Diagnostics.Debug.WriteLine(sokanKeisu);
            // 相関関数の逆行列
            var invR = sokanKeisu.Inverse();
            System.Diagnostics.Debug.WriteLine(invR);

            // MT距離を求める
            var mtDist = DenseMatrix.Create(data.RowCount, 1, 0);
            for (int y = 0; y < data.RowCount; y++)
            {
                var elm = data.Row(y).ToRowMatrix();
                var val = elm * invR * elm.Transpose() / data.ColumnCount;
                mtDist[y, 0] = val[0, 0];
            }
            System.Diagnostics.Debug.WriteLine(mtDist);

        }

        /// <summary>
        /// MT法テスト4項目
        /// </summary>
        private void TestMT4num()
        {
            var data = GetTestData4num();

            var cols = data.ToColumnArrays();

            // 列ごとの平均を求める
            var heikin = new double[]{
                                cols[0].Mean(),
                                cols[1].Mean(),
                                cols[2].Mean(),
                                cols[3].Mean()};

            // 各要素から平均をひく
            for (int x = 0; x < data.ColumnCount; x++)
            {
                for (int y = 0; y < data.RowCount; y++)
                {
                    data[y, x] -= heikin[x];
                }
            }

            // 列ごとの標準偏差を求める
            var hyojunHensa = new double[]{
                                cols[0].PopulationStandardDeviation(),
                                cols[1].PopulationStandardDeviation(),
                                cols[2].PopulationStandardDeviation(),
                                cols[3].PopulationStandardDeviation()};
            // 各要素を標準偏差で割る
            for (int x = 0; x < data.ColumnCount; x++)
            {
                for (int y = 0; y < data.RowCount; y++)
                {
                    data[y, x] /= hyojunHensa[x];
                }
            }

            // 相関係数を求める= 各要素を基準化((値 - 平均) / 標準偏差)したものに列
            var sokanKeisu = DenseMatrix.Create(4, 4, 0);
            for (int y = 0; y < data.RowCount; y++)
            {
                // γ11
                sokanKeisu[0, 0] += data[y, 0] * data[y, 0];
                // γ12
                sokanKeisu[1, 0] += data[y, 0] * data[y, 1];
                // γ13
                sokanKeisu[2, 0] += data[y, 0] * data[y, 2];
                // γ14
                sokanKeisu[3, 0] += data[y, 0] * data[y, 3];

                // γ21
                sokanKeisu[0, 1] += data[y, 1] * data[y, 0];
                // γ22
                sokanKeisu[1, 1] += data[y, 1] * data[y, 1];
                // γ23
                sokanKeisu[2, 1] += data[y, 1] * data[y, 2];
                // γ24
                sokanKeisu[3, 1] += data[y, 1] * data[y, 3];

                // γ31
                sokanKeisu[0, 2] += data[y, 2] * data[y, 0];
                // γ32
                sokanKeisu[1, 2] += data[y, 2] * data[y, 1];
                // γ33
                sokanKeisu[2, 2] += data[y, 2] * data[y, 2];
                // γ34
                sokanKeisu[3, 2] += data[y, 2] * data[y, 3];

                // γ41
                sokanKeisu[0, 3] += data[y, 3] * data[y, 0];
                // γ42
                sokanKeisu[1, 3] += data[y, 3] * data[y, 1];
                // γ43
                sokanKeisu[2, 3] += data[y, 3] * data[y, 2];
                // γ44
                sokanKeisu[3, 3] += data[y, 3] * data[y, 3];

            }
            // γ11
            sokanKeisu[0, 0] /= data.RowCount;
            // γ12
            sokanKeisu[1, 0] /= data.RowCount;
            // γ13
            sokanKeisu[2, 0] /= data.RowCount;
            // γ14
            sokanKeisu[3, 0] /= data.RowCount;

            // γ21
            sokanKeisu[0, 1] /= data.RowCount;
            // γ22
            sokanKeisu[1, 1] /= data.RowCount;
            // γ23
            sokanKeisu[2, 1] /= data.RowCount;
            // γ24
            sokanKeisu[3, 1] /= data.RowCount;

            // γ31
            sokanKeisu[0, 2] /= data.RowCount;
            // γ32
            sokanKeisu[1, 2] /= data.RowCount;
            // γ33
            sokanKeisu[2, 2] /= data.RowCount;
            // γ34
            sokanKeisu[3, 2] /= data.RowCount;

            // γ41
            sokanKeisu[0, 3] /= data.RowCount;
            // γ42
            sokanKeisu[1, 3] /= data.RowCount;
            // γ43
            sokanKeisu[2, 3] /= data.RowCount;
            // γ44
            sokanKeisu[3, 3] /= data.RowCount;

            System.Diagnostics.Debug.WriteLine(sokanKeisu);
            // 相関関数の逆行列
            var invR = sokanKeisu.Inverse();
            System.Diagnostics.Debug.WriteLine(invR);

            // MT距離を求める
            var mtDist = DenseMatrix.Create(data.RowCount, 1, 0);
            for (int y = 0; y < data.RowCount; y++)
            {
                var elm = data.Row(y).ToRowMatrix();
                var val = elm * invR * elm.Transpose() / data.ColumnCount;
                mtDist[y, 0] = val[0, 0];
            }
            System.Diagnostics.Debug.WriteLine(mtDist);

        }

        public static IEnumerable<double> CalcMTDistance(IEnumerable<IEnumerable<double>> src)
        {
            var data = DenseMatrix.OfColumns(src);
            return CalcMTDistance(data);
        }

        /// <summary>
        /// MT距離を求める
        /// </summary>
        private static IEnumerable<double> CalcMTDistance(DenseMatrix data)
        {
            var cols = data.ToColumnArrays();

            var heikin = new List<double>();
            for (int x = 0; x < data.ColumnCount; x++)
            {
                // 列ごとの平均を求める
                heikin.Add(cols[x].Mean());

                // 各要素を基準化する
                // 各要素から平均をひく
                for (int y = 0; y < data.RowCount; y++)
                {
                    data[y, x] -= heikin[x];
                }

                // 列ごとの標準偏差を求める
                var hyojunHensa = cols[x].PopulationStandardDeviation();

                if (hyojunHensa != 0)
                {
                    // 各要素を標準偏差で割る
                    for (int y = 0; y < data.RowCount; y++)
                    {
                        data[y, x] /= hyojunHensa;
                    }
                }
            }


            // 相関係数を求める= 各要素を基準化((値 - 平均) / 標準偏差)し、列の組み合わせで乗じる
            var sokanKeisu = DenseMatrix.Create(data.ColumnCount, data.ColumnCount, 0);
            for (int x = 0; x < data.ColumnCount; x++)
            {
                for (int y = 0; y < data.ColumnCount; y++)
                {
                    for (int n = 0; n < data.RowCount; n++)
                    {
                        sokanKeisu[y, x] += data[n, x] * data[n, y];
                    }
                }
            }
            for (int x = 0; x < data.ColumnCount; x++)
            {
                for (int y = 0; y < data.ColumnCount; y++)
                {
                    sokanKeisu[y, x] /= data.RowCount;
                }
            }

            System.Diagnostics.Debug.WriteLine(sokanKeisu);
            // 相関関数の逆行列
            var invR = sokanKeisu.Inverse();
            System.Diagnostics.Debug.WriteLine(invR);

            // MT距離を求める
            var mtDist = DenseMatrix.Create(data.RowCount, 1, 0);
            for (int y = 0; y < data.RowCount; y++)
            {
                var elm = data.Row(y).ToRowMatrix();
                var val = elm * invR * elm.Transpose() / data.ColumnCount;
                mtDist[y, 0] = val[0, 0];
            }
            System.Diagnostics.Debug.WriteLine(mtDist);

            return mtDist.Column(0);
        }

        public static void TestMT2()
        {
            var data3 = GetTestData3num();
            CalcMTDistance(data3);

            var data4 = GetTestData4num();
            CalcMTDistance(data4);

        }

        /// <summary>
        /// 平均値を求める
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static double CalcHeikinchi(IEnumerable<double> src)
        {
            return src.Average();
        }

        public static double CalcHyojunHensa(IEnumerable<double> src)
        {
            return src.PopulationStandardDeviation();
        }

    }
}
