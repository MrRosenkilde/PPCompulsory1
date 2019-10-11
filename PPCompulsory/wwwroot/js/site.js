// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function sequentielPrimeSearch()
{
    primesXMLRequest('primes/sequentiel');
}
function primesXMLRequest(baseQuery) {
    let from = document.getElementById('min').valueAsNumber;
    let to = document.getElementById('max').valueAsNumber;
    $.ajax({
        method: 'POST',
        url: baseQuery + '? from=' + from + ' &to=' + to,
    }).done((data) => {
        var element = document.createElement('a');
        element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(data));
        element.setAttribute('download', 'primes.txt');

        element.style.display = 'none';
        document.body.appendChild(element);

        element.click();

        document.body.removeChild(element);

    });
}

function parallelPrimeSearch()
{
    primesXMLRequest('primes/parallel');
}
function test(i) {
    return Promise.resolve()
        .then(function () {

            // update the DOM
            setTimeout(function () {
                document.getElementById('results').innerHTML += i;
            }, 0);

            return i;
        });
}