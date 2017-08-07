import _ from 'lodash'
import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import ReactPaginate from 'react-paginate'
import {addLocaleData, FormattedRelative, IntlProvider } from 'react-intl'
import en from 'react-intl/locale-data/en'
import pt from 'react-intl/locale-data/pt'

import * as ligacaoActions from '../actions/ligacao'
import * as actions from '../actions'

addLocaleData([...en, ...pt]);

let cont = 0;

class LigacaoPage extends Component {
    constructor(props) {
        super(props);

        this.state = {
            showResponder: 0,
            retorno: '',
            search: '',
            lastCheck: null,
            savingResponder: 0
        };
    }

    componentDidMount = () => {
        this.props.fetchLigacoes(this.state.search, 1);
        this.setState({lastCheck: new Date()});
    }

    componentWillUnmount() {
        clearTimeout(this.timeout);
    }

    componentWillReceiveProps = (nextProps) => {
        if(nextProps.isLoading) return;

        this.setState({savingResponder: 0});

        clearTimeout(this.timeout);
        this.timeout = setTimeout(() => {
            this.props.checkNewLigacoes(this.state.lastCheck);
        }, 9500);
    }

    handlePageClick = data => {
        if(this.props.ligacoes.current_page - 1 != data.selected) {
            this.props.fetchLigacoes(this.state.search, data.selected + 1);
        }
    };

    handleLigacaoClick = ligacao => {
        this.props.getLigacao(ligacao);
    }

    handleRetornoClick = id => {
        this.setState({ showResponder: id, retorno: '' });
    }

    handleSaveLigacaoClick = ligacao => {
        this.setState({ showResponder: 0, retorno: '', savingResponder: ligacao.id });
        this.props.responderLigacao(ligacao.id, this.state.retorno);
    }

    printRetorno(ligacao) {
        if(ligacao.retorno == null || ligacao.retorno == "")
        {
            if(this.state.showResponder == ligacao.id) {
                return (
                    <div>
                        <textarea placeholder="Retorno" className="form-control" ref={input => {if(input) input.focus()}} value={this.state.retorno} onChange={event => this.setState({ retorno: event.target.value })}></textarea>
                        <div className="pull-right">
                            <button type="button" className="btn btn-sm btn-primary" onClick={event => this.handleSaveLigacaoClick(ligacao)}><i className="fa fa-dot-circle-o"></i> Salvar</button>
                            <button type="button" className="btn btn-sm btn-default" onClick={event => this.setState({ showResponder: 0, retorno: '' })}><i className="fa fa-ban"></i> Cancelar</button>
                        </div>
                    </div>
                );
            }
            else {
                if(this.state.savingResponder == ligacao.id) {
                    return (
                         <div>
                            <i className="fa fa-refresh fa-spin"></i> Salvando retorno ...
                         </div>
                    )
                }
            }
            return (<button type="button" className="btn btn-link text-success" onClick={event => this.handleRetornoClick(ligacao.id)}><i className="fa fa-link"></i> Responder</button>);
        }
        return ligacao.retorno;
    }

    printNovasLigacoesMessage() {
        if(this.props.ligacoes.news > 1) {
            return <span>Existem {this.props.ligacoes.news} novas ligações, clique para atualizar a tabela</span>;
        }
        return <span>Existe {this.props.ligacoes.news} nova ligação, clique para atualizar a tabela</span>;

    }

    printNovasLigacoes() {
        if(this.props.ligacoes.news != null && this.props.ligacoes.news > 0) {
            return (<div className="alert alert-warning show fade" role="alert" onClick={event => {this.props.cleanNewLigacoes(); this.props.fetchLigacoes(this.state.search, 1); this.setState({lastCheck: new Date()});}}>
                <strong>Atenção!</strong> {this.printNovasLigacoesMessage()}
            </div>);
        }
        return '';
    }

    printMadreCardInfo(madrecard) {
      if(madrecard != null && madrecard.numero != null && madrecard.numero != '') {
        return <span className={madrecard.ativo ? "badge badge-success" : "badge badge-danger"}>{madrecard.numero}</span>;
      }
      return <span>Não possui</span>;
    }

    printLigacaoData() {
        if(this.props.isLoading) {
            return <tr><td colSpan="3" style={{textAlign:"center"}}><i className="fa fa-refresh fa-spin"></i> Carregando ...</td></tr>;
        }

        if(!this.props.isLoading && _.isEmpty(this.props.ligacoes.data)) {
            return <tr><td colSpan="3" style={{textAlign:"center"}}>Nenhum item encontrado</td></tr>;
        }
        return _.map(_.orderBy(this.props.ligacoes.data, 'created_at', 'desc'), ligacao => {
            return (
                <tr key={ligacao.id}>
                    <td>{ligacao.nome}</td>
                    <td>{ligacao.telefone}</td>
                    <td>{this.printMadreCardInfo(ligacao.madrecard)}</td>
                    <td>{this.printRetorno(ligacao)}</td>
                    <td>
                        <IntlProvider locale={navigator.language}><FormattedRelative value={new Date(ligacao.created_at.replace(' ', 'T'))} /></IntlProvider></td>
                </tr>
            );
        });
    }

    render() {
        return (
            <div className="animated fadeIn">
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <i className="fa fa-align-justify"></i> Ligações recebidas
                            </div>
                            <div className="card-block">
                            <div>
                                {this.printNovasLigacoes()}
                            </div>
                                <table className="table table-bordered table-striped table-sm">
                                    <thead>
                                        <tr>
                                            <th>Nome</th>
                                            <th>Telefone</th>
                                            <th>MadreCard</th>
                                            <th>Resposta</th>
                                            <th>Data</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.printLigacaoData()}
                                    </tbody>
                                </table>
                                <nav>
                                    <ReactPaginate previousLabel={"Anterior"}
                                        nextLabel={"Próximo"}
                                        breakLabel={<a href="">...</a>}
                                        breakClassName={"break-me"}
                                        initialPage={this.props.ligacoes.current_page - 1}
                                        forcePage={this.props.ligacoes.current_page - 1}
                                        pageCount={this.props.ligacoes.last_page}
                                        marginPagesDisplayed={2}
                                        pageRangeDisplayed={5}
                                        onPageChange={this.handlePageClick}
                                        containerClassName={"pagination"}
                                        subContainerClassName={"page-item"}
                                        pageLinkClassName={"page-link"}
                                        activeClassName={"active"} />
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {
    ligacoes: state.ligacoes,
    isLoading: state.isLoading
}};

const mapDispatchToProps = (dispatch) => bindActionCreators({...actions, ...ligacaoActions}, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(LigacaoPage);
