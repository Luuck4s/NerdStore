## NerdStore 
> Projeto desenvolvido no curso de Modelagem de Domínios Ricos do Desenvolvedor.io

Projeto de loja virtual utilizando conceitos de DDD, CQRS e Event Sourcing.
O projeto está separado em alguns módulos sendo os mais importantes:
- Catalogo
  - Responsável por cadastro de produtos
- Pagamentos
  - Responsável por pagamentos
- Vendas
  - Responsável por realizar os carrinhos de compras
- Core
- Api
  - Todos os outros modulos estão sendo importados e configurados para executarem em um único container.


### Ambiente

- [EventStore < 5.0.x](https://www.eventstore.com/)