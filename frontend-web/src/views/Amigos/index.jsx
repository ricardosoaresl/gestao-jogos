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

export default function Amigos(props) {
  const [amigos, setAmigos] = useState([]);

  useEffect(() => {
    handleGetAmigos();
  }, []);

  const handleGetAmigos = () => {
    BaseCrud.get("amigo?$orderBy=Id").then((response) => {
      setAmigos(response.data.value);
    });
  }

  const handleDelete = (id) => {
    confirmAlert({
      //title: 'Alerta',
      message: 'Tem certeza que deseja deletar esse registro?',
      buttons: [
        {
          label: 'Sim, deletar!',
          onClick: () => deletarAmigo(id)
        },
        {
          label: 'Cancelar',
        }
      ]
    });
  };

  const deletarAmigo = (id) => {
    BaseCrud.delete("amigo", id).then((response) => {
      handleGetAmigos();
      toast.success("Amigo deletado com sucesso!");
    });
  }

  return (
    <div className="content">
      <ToastContainer autoClose={5000} />
      <Row>
        <Col md={12} style={{ marginBottom: 15 }}>
          <Link
            to={`/admin/amigos-editar`}
            className="btn btn-clean btn-md btn-icon"
            title="Cadastrar Amigo"
            style={{ marginLeft: 15 }}
          >
            Cadastrar Amigo
              </Link>
        </Col>
      </Row>
      <Row>
        <Col md={12}>
          <Card
            title="Amigos"
            category="Lista de amigos cadastrados no sistema"
            ctTableFullWidth
            ctTableResponsive
            content={
              <Table striped hover>
                <thead>
                  <tr>
                    <th>Id</th>
                    <th>Nome</th>
                    <th>Data Cadastro</th>
                    <th>Acao</th>
                  </tr>
                </thead>
                <tbody>
                  {amigos.map((amigo, key) => {
                    return (
                      <tr key={key}>
                        <td>{amigo.Id}</td>
                        <td>{amigo.Nome}</td>
                        <td>{amigo.DataCadastro}</td>
                        <td>
                          <Link
                            to={`/admin/amigos-editar/${amigo.Id}`}
                            className="btn btn-clean btn-md btn-icon"
                            title="Editar"
                            style={{ marginRight: 5 }}
                          >
                            Editar
                              </Link>
                          <Button onClick={() => handleDelete(amigo.Id)}>
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