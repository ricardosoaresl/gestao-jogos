import React, { useRef, useState, useEffect } from "react";
import { Row, Col } from "react-bootstrap";

import { StatsCard } from "components/StatsCard/StatsCard.jsx";

import BaseCrud from "../crud/base.crud";

export default function Dashboard() {
  const [amigos, setAmigos] = useState([]);
  const [jogos, setJogos] = useState([]);

  useEffect(() => {
    BaseCrud.get(`amigo`).then((response) => {
      if (response.data.value) {
        setAmigos(response.data.value);
      }
    });

    BaseCrud.get(`jogo`).then((response) => {
      if (response.data.value) {
        setJogos(response.data.value);
      }
    });
  }, []);

  return (
    <div className="content">
      <Row>
        <Col lg={3} sm={6}>
          <StatsCard
            bigIcon={<i className="fa fa-user text-warning" />}
            statsText="Amigos"
            statsValue={amigos.length > 0 ? amigos.length : 0}
            statsIcon={<i className="fa fa-refresh" />}
            statsIconText="Confira sua lista de amigos"
            style={{ textAlign: "center" }}
          />
        </Col>
        <Col lg={3} sm={6}>
          <StatsCard
            bigIcon={<i className="fa fa-play text-success" />}
            statsText="Jogos"
            statsValue={jogos.length > 0 ? jogos.length : 0}
            statsIcon={<i className="fa fa-refresh" />}
            statsIconText="Confira sua lista de jogos"
          />
        </Col>
        <Col lg={3} sm={6}>
          <StatsCard
            bigIcon={<i className="fa fa-close text-danger" />}
            statsText="Jogos Emprestados"
            statsValue={jogos.filter(x => x.Amigo != null).length > 0 ? jogos.filter(x => x.Amigo != null).length : 0}
            statsIcon={<i className="fa fa-refresh" />}
            statsIconText="Veja os jogos que você emprestou"
          />
        </Col>
        <Col lg={3} sm={6}>
          <StatsCard
            bigIcon={<i className="fa fa-check text-info" />}
            statsText="Jogos Disponíveis"
            statsValue={jogos.filter(x => x.Amigo == null).length > 0 ? jogos.filter(x => x.Amigo == null).length : 0}
            statsIcon={<i className="fa fa-refresh" />}
            statsIconText="Veja os jogos que estão disponíveis para jogar"
          />
        </Col>
      </Row>
    </div>
  );
}
