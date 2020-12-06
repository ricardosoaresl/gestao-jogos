import React, { useState, useEffect } from "react";
import { Row, Col, Table, Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';
import { toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer } from "react-toastify";

import Card from "components/Card/Card.jsx";

import BaseCrud from "../../crud/base.crud";

export default function Jogos(props) {
  const [jogos, setJogos] = useState([]);

  useEffect(() => {
    handleGetJogos();
  }, []);

  const handleGetJogos = () => {
    BaseCrud.get("jogo?$orderBy=Id").then((response) => {
      setJogos(response.data.value);
    });
  }

  const handleDelete = (id) => {
    confirmAlert({
      //title: 'Alerta',
      message: 'Tem certeza que deseja deletar esse registro?',
      buttons: [
        {
          label: 'Sim, deletar!',
          onClick: () => deletarJogo(id)
        },
        {
          label: 'Cancelar',
        }
      ]
    });
  };

  const deletarJogo = (id) => {
    BaseCrud.delete("jogo", id).then((response) => {
      handleGetJogos();
      toast.success("Jogo deletado com sucesso!");
    });
  }

  return (
    <div className="content">
      <ToastContainer autoClose={5000} />
      <Row>
        <Col md={12} style={{ marginBottom: 15 }}>
          <Link
            to={`/admin/jogos-editar`}
            className="btn btn-clean btn-md btn-icon"
            title="Cadastrar Jogo"
            style={{ marginLeft: 15 }}
          >
            Cadastrar Jogo
              </Link>
        </Col>
      </Row>
      <Row>
        <Col md={12}>
          <Card
            title="Jogos"
            category="Lista de jogos cadastrados no sistema"
            ctTableFullWidth
            ctTableResponsive
            content={
              <Table striped hover>
                <thead>
                  <tr>
                    <th>Id</th>
                    <th>Nome</th>
                    <th>Emprestado para</th>
                    <th>Data Cadastro</th>
                    <th>Acao</th>
                  </tr>
                </thead>
                <tbody>
                  {jogos.map((jogo, key) => {
                    return (
                      <tr key={key}>
                        <td>{jogo.Id}</td>
                        <td>{jogo.Nome}</td>
                        <td>{jogo.AmigoId ? jogo.Amigo.Nome : "--"}</td>
                        <td>{jogo.DataCadastro}</td>
                        <td>
                          <Link
                            to={`/admin/jogos-editar/${jogo.Id}`}
                            className="btn btn-clean btn-md btn-icon"
                            title="Editar"
                            style={{ marginRight: 5 }}
                          >
                            Editar
                              </Link>
                          <Button onClick={() => handleDelete(jogo.Id)}>
                            Excluir
                              </Button>
                        </td>
                      </tr>
                    );
                  })}
                </tbody>
              </Table>
            }
          />
        </Col>
      </Row>
    </div>
  );
}