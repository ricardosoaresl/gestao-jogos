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

export default function Amigos(props, values, handleChange) {

  const [amigo, setAmigo] = useState({
    Id: "",
    Nome: ""
  });

  useEffect(() => {
    let id = props.location.pathname.split("/")[3]; // Configurar React Route
    if (id && id.length > 0) {
      BaseCrud.get(`amigo/${id}`).then((response) => {
        if (response.data) {
          setAmigo(response.data);
        }
      });
    }
  }, []);

  const Guid = () => {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  const handleSubmit = function (v) {
    if (amigo.Id) {
      let changeAmigo = {
        Id: amigo.Id,
        Nome: v.Nome
      };

      editar(changeAmigo).then(r => {
        toast.success("Operação realizada com sucesso");
        setTimeout(() => {
          props.history.push('/admin/amigos')
        }, 3000);
      }).catch(e => { });
    } else {
      incluir({
        Id: Guid(),
        Nome: v.Nome
      }).then(r => {
        toast.success("Operação realizada com sucesso");
        setTimeout(() => {
          props.history.push('/admin/amigos')
        }, 3000);
      }).catch(e => { });
    }

  };

  const editar = async (v) => BaseCrud.put("Amigo", v.Id, v);

  const incluir = async (v) => BaseCrud.post("Amigo", v);

  return (
    <div className="content">
      <ToastContainer autoClose={5000} />
      <Row>
        <Col md={12}>
          <Card
            title="Editar Amigo"
            content={
              <form onSubmit={(e) => handleSubmit(e)}>
                {(amigo && (
                  <Formik
                    enableReinitialize={true}
                    initialValues={amigo}
                    onSubmit={handleSubmit}
                  >
                    {({ errors, values, touched, handleChange, handleSubmit }) => (
                      <Form onSubmit={(e) => e.preventDefault()}>
                        <Row style={{ marginBottom: 10 }}>
                          {amigo && amigo.Id && (
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

                          <Col md={12}>
                            <Link
                              to={`/admin/amigos`}
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
              </form>
            }
          />
        </Col>
      </Row>
    </div>
  );
}
