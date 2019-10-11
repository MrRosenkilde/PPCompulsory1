// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function sequentielPrimeSearch()
{
    primesXMLRequest('primes/sequentiel');
}
function primesXMLRequest(baseQuery) {

    document.getElementById('results').innerHTML = "Copy content from file here, to see the results";

    let from = document.getElementById('min').valueAsNumber;
    let to = document.getElementById('max').valueAsNumber;
    var element = document.createElement('a');
    element.setAttribute('href', baseQuery + '?from='+from+'&to='+to);
    //element.setAttribute('download', 'primes.txt');

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
}

function parallelPrimeSearch()
{
    primesXMLRequest('primes/parallel');
}
