$(document).ready(function () {
    $('#tableEnc').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.11.5/i18n/pt-BR.json',
            lengthMenu: '_MENU_ encomendas por página',
            info: '_PAGE_ de _PAGES_ páginas',
            zeroRecords: 'Nenhuma encomenda encontrada com esses critérios. Tente usar outros valores na pesquisa.',
            infoFiltered: ' encontradas. (Total de _MAX_)',
            infoEmpty: '0 ',
            paginate: {
                first: '1',                
                next: '>',
                previous: '<'
                },
        },
        columnDefs: [
            {
                target: 5,
                searchable: false,
                orderable: false,
            }],
    }); 
        

    $('#tableCli').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.11.5/i18n/pt-BR.json',
            lengthMenu: '_MENU_ clientes por página',
            info: '_PAGE_ de _PAGES_ páginas',
            zeroRecords: 'Nenhum cliente encontrado com esses critérios. Tente usar outros valores na pesquisa.',
            infoFiltered: ' encontrados. (Total de _MAX_)',
            infoEmpty: '0 ',
            paginate: {
                first: '1',
                next: '>',
                previous: '<'
            },
        },
        columnDefs: [
            {
                target: 5,
                searchable: false,
                orderable: false,
            }],
    });
});