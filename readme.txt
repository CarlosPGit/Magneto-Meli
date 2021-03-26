NOTA: dejare una carpeta con los test de los servicios desde postman en formato json para importar.
      revisar carpeta Postman/Magneto.postman_collection

* Si la ejecucion es online  
    1. Ejecucion de las apis para nivel 2
      POST => http://capg7338-001-site1.htempurl.com/mutant
      ///////////////////////////////////////////////////////////////////////////////////////////
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        var raw = JSON.stringify({"dna":["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]});

        var requestOptions = {
          method: 'POST',
          headers: myHeaders,
          body: raw,
          redirect: 'follow'
        };

        fetch("http://capg7338-001-site1.htempurl.com/mutant", requestOptions)
          .then(response => response.text())
          .then(result => console.log(result))
          .catch(error => console.log('error', error));
      ///////////////////////////////////////////////////////////////////////////////////////////

    2. Ejecucion de las apis para nivel 2
      GET => http://capg7338-001-site1.htempurl.com/stats
  
* Si la ejecucion es local, la bd continuara estando en la nube
    1. Ejecucion de las apis para nivel 2
      POST => https://localhost:44369/mutant
      ///////////////////////////////////////////////////////////////////////////////////////////
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        var raw = JSON.stringify({"dna":["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]});

        var requestOptions = {
          method: 'POST',
          headers: myHeaders,
          body: raw,
          redirect: 'follow'
        };

        fetch("https://localhost:44369/mutant", requestOptions)
          .then(response => response.text())
          .then(result => console.log(result))
          .catch(error => console.log('error', error));
      ///////////////////////////////////////////////////////////////////////////////////////////

    2. Ejecucion de las apis para nivel 2
      GET => https://localhost:44369/stats
