import React, { useRef, useState, useEffect } from "react";
import { toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer } from "react-toastify";
import { Link } from "react-router-dom";
import {
  Row,
  Col,
  Form,
} from "react-bootstrap";
import * as Yup from "yup";
import { Formik } from "formik";

import { Card } from "components/Card/Card.jsx";
import Button from "components/CustomButton/CustomButton.jsx";

import BaseCrud from "../../crud/base.crud";

const schema = Yup.object().shape({
  Nome: Yup.string().required("Campo obrigatório"),
});

export default function Jogos(props, values, handleChange) {

  const [jogo, setJogo] = useState({
    Id: null,
    Nome: "",
    AmigoId: null
  });

  const [amigos, setAmigos] = useState([]);

  useEffect(() => {
    let id = props.location.pathname.split("/")[3]; // Configurar React Route
    if (id && id.length > 0) {
      BaseCrud.get(`jogo/${id}`).then((response) => {
        if (response.data) {
          setJogo(response.data);
        }
      });

    }

    BaseCrud.get(`amigo`).then((response) => {
      setAmigos(response.data.value);
    });

  }, []);

  const Guid = () => {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  const handleSubmit = function (v) {
    if (jogo.Id) {
      editar({
        Id: jogo.Id,
        Nome: v.Nome,
        AmigoId: v.AmigoId
      }).then(r => {
        toast.success("Operação realizada com sucesso");
        setTimeout(() => {
          props.history.push('/admin/jogos')
        }, 3000);
      }).catch(e => { });
    } else {
      incluir({
        Id: Guid(),
        Nome: v.Nome,
        AmigoId: v.AmigoId
      }).then(r => {
        toast.success("Operação realizada com sucesso");
        setTimeout(() => {
          props.history.push('/admin/jogos')
        }, 3000);
      }).catch(e => { });
    }

  };

  const editar = async (v) => BaseCrud.put("Jogo", v.Id, v);

  const incluir = async (v) => BaseCrud.post("Jogo", v);

  return (
    <div className="content">
      <ToastContainer autoClose={5000} />
      <Row>
        <Col md={12}>
          <Card
            title="Editar Jogo"
            content={
              <>
                {(jogo && (
                  <Formik
                    enableReinitialize={true}
                    initialValues={jogo}
                    onSubmit={handleSubmit}
                  >
                    {({ errors, values, touched, handleChange, handleSubmit }) => (
                      <Form onSubmit={(e) => e.preventDefault()}>
                        <Row style={{ marginBottom: 10 }}>
                          {jogo && jogo.Id && (
                            <Col md={3}>
                              <Form.Label>Código</Form.Label>
                              <Form.Control
                                type="text"
                                name="Id"
                                placeholder="Código"
                                value={values.Id}
                                onChange={handleChange}
                              />
                            </Col>
                          )}
                          <Col md={3}>
                            <Form.Label>Nome</Form.Label>
                            <Form.Control
                              type="text"
                              name="Nome"
                              placeholder="Nome"
                              value={values.Nome}
                              onChange={handleChange}
                            />
                          </Col>

                          {amigos.length > 0 && (
                            <Col md={3}>
                              <Form.Label>Emprestado Para</Form.Label>
                              <Form.Control
                                as="select"
                                name="AmigoId"
                                value={values.AmigoId}
                                onChange={handleChange}
                              >
                                <option value="">Selecione</option>
                                {amigos.map(
                                  (amigo) => (
                                    <option
                                      key={amigo.Id}
                                      value={amigo.Id}
                                      onChange={handleChange}
                                    >
                                      {amigo.Nome}
                                    </option>
                                  )
                                )}
                              </Form.Control>
                            </Col>
                          )}

                          <Col md={12}>
                            <Link
                              to={`/admin/jogos`}
                              className="btn btn-clean btn-md btn-icon"
                              title="Voltar"
                              style={{ marginRight: 5 }}
                            >
                              Voltar
                            </Link>
                            <Button
                              variant="primary"
                              className="ml-2"
                              onClick={handleSubmit}>
                              Salvar
                            </Button>
                          </Col>
                        </Row>
                      </Form>
                    )}
                  </Formik>
                ))}
              </>
            }
          />
        </Col>
      </Row>
    </div>
  );
}
