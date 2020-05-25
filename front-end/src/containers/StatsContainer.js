import React, { useEffect, useState } from "react";
import { Card } from "react-bootstrap";
import { Pie, Bar } from "react-chartjs-2";
import { getAllStatsData } from "../services/api";

const initialPieData = {
  labels: ["", ""],
  datasets: [
    {
      data: [80, 20],
      backgroundColor: ["#7ae021", "#ce462a"],
    },
  ],
};

const initialBarData = {
  labels: [""],
  datasets: [
    {
      label: "",
      backgroundColor: "#71dce4",
      barPercentage: 0.5,
      barThickness: 6,
      maxBarThickness: 8,
      minBarLength: 2,
      data: [{ x: 1, y: 1 }],
    },
  ],
};

const StatsContainer = () => {
  const [allStats, setAllStats] = useState([]);
  const [avgTime, setAvgTime] = useState("");
  const [effectiveness, setEffectiveness] = useState(0);

  const getStatsData = async () => {
    const apiData = await getAllStatsData();

    setAllStats(apiData.data.chartsData);
    setAvgTime(apiData.data.averageTime);
    setEffectiveness(apiData.data.effectiveness);
  };

  useEffect(() => {
    getStatsData();
  }, []);

  const prepareDataToPieChart = (chartData) =>
    chartData
      ? {
          labels: chartData.item1,
          datasets: [
            {
              data: chartData.item2,
              backgroundColor: ["#7ae021", "#ce462a"],
            },
          ],
        }
      : initialPieData;

  const prepareDataToBarChart = (chartData) =>
    chartData
      ? {
          labels: chartData.item1,
          datasets: [
            {
              label: "liczba",
              backgroundColor: "#71dce4",
              barPercentage: 0.5,
              barThickness: 6,
              maxBarThickness: 8,
              minBarLength: 2,
              data: chartData.item2.map((yVal, i) => ({ x: i, y: yVal })),
            },
          ],
        }
      : initialBarData;

  return (
    <div className="stats-container">
      <div className="card-custom">
        <p>
          Średni czas przetwarzania obrazu: <b>{avgTime} ms</b>
        </p>
        <p>
          Średnia efektywność znajdowania obiektów:{" "}
          <b>{(effectiveness * 100).toFixed(2)}%</b>
        </p>
      </div>
      {allStats &&
        allStats.map((stat, i) => {
          const card = (
            <Card key={i} className="card-custom stats-card ">
              <h3>{stat.title}</h3>
              {stat.chartType === "doughnut" ? (
                <Pie
                  data={prepareDataToPieChart(stat.data)}
                  options={{ maintainAspectRatio: true }}
                />
              ) : (
                <Bar
                  data={prepareDataToBarChart(stat.data)}
                  options={{
                    maintainAspectRatio: true,
                    scales: {
                      yAxes: [
                        {
                          ticks: {
                            beginAtZero: true,
                            min: 0,
                          },
                        },
                      ],
                    },
                  }}
                />
              )}
            </Card>
          );

          return card;
        })}
    </div>
  );
};

export default StatsContainer;
