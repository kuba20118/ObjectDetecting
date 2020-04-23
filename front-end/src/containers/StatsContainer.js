import React from "react";
import { Table, Row, Card, Col } from "react-bootstrap";
import { Pie, Bar } from "react-chartjs-2";

const StatsContainer = () => {
  const pieData = {
    labels: ["Wykryto", "Nie wykryto"],
    datasets: [
      {
        data: [80, 20],
        backgroundColor: ["#7ae021", "#ce462a"],
      },
    ],
  };

  const barData = {
    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
    datasets: [
      {
        label: "Liczba wykrytych (średnio)",
        backgroundColor: "#71dce4",
        barPercentage: 0.5,
        barThickness: 6,
        maxBarThickness: 8,
        minBarLength: 2,
        data: [
          { x: 1, y: 1 },
          { x: 2, y: 2 },
          { x: 3, y: 3 },
          { x: 4, y: 4 },
          { x: 5, y: 5 },
          { x: 6, y: 6 },
          { x: 7, y: 7 },
          { x: 8, y: 8 },
          { x: 9, y: 8 },
          { x: 10, y: 9 },
          { x: 11, y: 9 },
          { x: 12, y: 10 },
        ],
      },
    ],
  };

  return (
    <div style={{ paddingTop: "15px" }}>
      <Row>
        <Col md={5}>
          <Card className="card-custom">
            <h3>Wynik - wykrytych obiektów %</h3>

            <Pie data={pieData} options={{ maintainAspectRatio: true }} />
          </Card>
        </Col>
        <Col md={7}>
          <Card className="card-custom">
            <h3>Wykrytych obiektów średnio</h3>

            <Bar data={barData} options={{ maintainAspectRatio: true }} />
          </Card>
        </Col>
      </Row>

      <Row>
        <Card className="card-custom" style={{ width: "100%" }}>
          <h3>Historia</h3>
          <Table className="table-bordered table-striped">
            <thead>
              <tr>
                <th>#</th>
                <th>Data</th>
                <th>Ilość obektów</th>
                <th>Wykryto (%)</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>1</td>
                <td>23-04-2020</td>
                <td>5</td>
                <td>80%</td>
              </tr>
              <tr>
                <td>2</td>
                <td>22-04-2020</td>
                <td>12</td>
                <td>60%</td>
              </tr>
              <tr>
                <td>3</td>
                <td>21-04-2020</td>
                <td>3</td>
                <td>90%</td>
              </tr>
            </tbody>
          </Table>
        </Card>
      </Row>
    </div>
  );
};

export default StatsContainer;
