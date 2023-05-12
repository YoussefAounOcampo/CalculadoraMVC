'use strict';

/**
 * CalculadoraBasica class.
 * Define las operaciones básicas para una calculadora.
 */
function CalculadoraBasica() {
    this.basicOperationShape = new RegExp("(([1-9][0-9]*|[0.])(.[0-9]*[1-9])?[\-\+\*\/])(([1-9][0-9]*|[0.])(.[0-9]*[1-9])?)");
    this.memoryRegister = 0;
}


/**
 * Imprime el contenido de la memoria en la pantalla.
 */
CalculadoraBasica.prototype.printMemoryContents = function () {
    this.clearDisplay();
    this.writeToDisplay(this.memoryRegister);
}


/**
 * Resta el resultado de la operación actual al contenido de la memoria.
 */
CalculadoraBasica.prototype.subtractFromMemory = function () {
    this.memoryRegister -= this.solveOperation();
}


/**
 * Agrega el resultado de la operación actual al contenido de la memoria.
 */
CalculadoraBasica.prototype.addToMemory = function () {
    this.memoryRegister += this.solveOperation();
}


/**
 * Escribe datos en la pantalla.
 * @param {string} data - Datos a escribir en la pantalla.
 */
CalculadoraBasica.prototype.writeToDisplay = function (data) {
    var anterior = document.getElementById("displayBox").value;
    if (data == ".") {
        anterior += data;
    } else {
        anterior = anterior == "0" ? data : anterior += data;
    }
    document.getElementById("displayBox").value = anterior;
}


/**
 * Escribe un operador en la pantalla.
 * @param {string} operator - Operador a escribir en la pantalla.
 */
CalculadoraBasica.prototype.writeOperatorToDisplay = function (operator) {
    var anterior = document.getElementById("displayBox").value;
    if (this.basicOperationShape.test(anterior)) {
        this.solveOperation();
    }
    this.writeToDisplay(operator);
}


/**
 * Limpia el contenido de la pantalla.
 */
CalculadoraBasica.prototype.clearDisplay = function () {
    const displayBox = document.getElementById("displayBox");
    const displayContents = displayBox.value;
    if (displayContents) {
        displayBox.value = "0";
        this.operationString = "";
    }
}


/**
 * Limpia todos los contenidos, incluida la memoria.
 */
CalculadoraBasica.prototype.clearAll = function () {
    this.memoryRegister = 0;
    this.clearDisplay();
    this.operationString = "";
}


/**
 * Resuelve la operación actual y muestra el resultado en la pantalla.
 * @returns {number} El resultado de la operación.
 */
CalculadoraBasica.prototype.solveOperation = function () {
    var operation = document.getElementById("displayBox").value;
    var parts = operation.match(this.basicOperationShape);
    var result = 0;

    if (parts) {
        var num1 = parseFloat(parts[1]);
        var num2 = parseFloat(parts[4]);
        var operator = parts[3];

        switch (operator) {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                result = num1 / num2;
                break;
        }
    } else {
        alert("Syntax error");
        this.clearDisplay();
    }

    document.getElementById("displayBox").value = result;
    return result;
}


/**

CalculadoraCientifica class.
Extiende la funcionalidad de CalculadoraBasica para incluir operaciones científicas.
*/
function CalculadoraCientifica() {
    CalculadoraBasica.call(this);
    this.displayBox = document.getElementById("displayBox");
    this.displayBox.value = "0";
    this.inputList = new Array();
    this.operationString = "";
    this.justSolved = false;
    this.operationMap = {
        "sin(": "Math.sin(",
        "cos(": "Math.cos(",
        "tan(": "Math.tan(",
        "log(": "Math.log10(",
        "ln(": "Math.log(",
        "sqrt(": "Math.sqrt(",
        "PI": "Math.PI",
        "e": "Math.E"
    };
}

CalculadoraCientifica.prototype = Object.create(CalculadoraBasica.prototype);
CalculadoraCientifica.prototype.constructor = CalculadoraCientifica;


/**
 * Escribe datos en la pantalla y actualiza la cadena de operaciones.
 * @param {string} data - Datos a escribir en la pantalla.
 */
CalculadoraCientifica.prototype.writeToDisplay = function (data) {
    var displayBox = document.getElementById("displayBox");

    if (displayBox.value == "Syntax Error") {
        CalculadoraBasica.prototype.clearDisplay.call(this);
    }

    CalculadoraBasica.prototype.writeToDisplay.call(this, data);
    this.operationString += data;
    this.inputList.push(data);

    if (!this.isOperator(data) && !this.hasUnclosedParentheses() && !this.hasConsecutiveOperators()) {
        displayBox.classList.remove("red");
    } else {
        displayBox.classList.add("red");
    }
}


/**
 * Escribe un operador en la pantalla y actualiza la cadena de operaciones.
 * @param {string} operator - Operador a escribir en la pantalla.
 */
CalculadoraCientifica.prototype.writeOperatorToDisplay = function (operator) {
    if (document.getElementById("displayBox").value == "Syntax Error") {
        CalculadoraBasica.prototype.clearDisplay.call(this);
    }

    this.operationString += operator;
    CalculadoraBasica.prototype.writeToDisplay.call(this, operator);
    this.inputList.push(operator);

    var displayBox = document.getElementById("displayBox");
    if (this.isOperator(operator) || this.hasUnclosedParentheses() || this.hasConsecutiveOperators()) {
        displayBox.classList.add("red");
    } else {
        displayBox.classList.remove("red");
    }
}

/**
 * Verifica si un carácter es un operador.
 * @param {string} character - Carácter a verificar.
 * @returns {boolean} - true si es un operador, false de lo contrario.
 */
CalculadoraCientifica.prototype.isOperator = function (character) {
    var operators = ["+", "-", "*", "/"]; // Agrega aquí otros operadores si es necesario
    return operators.includes(character);
}

/**
 * Verifica si hay paréntesis sin cerrar en la cadena de operaciones.
 * @returns {boolean} - true si hay paréntesis sin cerrar, false de lo contrario.
 */
CalculadoraCientifica.prototype.hasUnclosedParentheses = function () {
    var openParentheses = 0;
    var closedParentheses = 0;

    for (var i = 0; i < this.operationString.length; i++) {
        if (this.operationString[i] === "(") {
            openParentheses++;
        } else if (this.operationString[i] === ")") {
            closedParentheses++;
        }
    }

    return openParentheses > closedParentheses;
}

/**
 * Verifica si hay operadores consecutivos en la cadena de operaciones.
 * @returns {boolean} - true si hay operadores consecutivos, false de lo contrario.
 */
CalculadoraCientifica.prototype.hasConsecutiveOperators = function () {
    var operators = ["+", "-", "*", "/"];
    var consecutiveOperators = false;

    for (var i = 0; i < this.operationString.length - 1; i++) {
        if (operators.includes(this.operationString[i]) && operators.includes(this.operationString[i + 1])) {
            consecutiveOperators = true;
            break;
        }
    }

    return consecutiveOperators;
}


/**

Resuelve la operación actual y muestra el resultado en la pantalla.

@returns {*} El resultado de la operación o "Syntax Error" si hay un error en la cadena de operaciones.
*/
CalculadoraCientifica.prototype.solveOperation = function () {
    var result = 0;
    try {
        const operationFunction = new Function(`return ${this.operationString}`);
        result = operationFunction();
        
        // Hacer la llamada AJAX
        $.ajax({
            type: 'POST',
            url: '/Calculadora/RegisterOperation',
            data: { OperationString: this.operationString, Resultado: result },
            success: function (data) {
                console.log('Operación registrada en la base de datos.');
            },
            error: function (xhr, status, error) {
                console.log('Error al registrar la operación en la base de datos.');
            }
        });
    } catch (err) {
        displayBox.classList.add("red");
        result = "Syntax Error";
        $.ajax({
            type: 'POST',
            url: '/Calculadora/RegisterOperation',
            data: { OperationString: this.operationString, Resultado: result },
            success: function (data) {
                console.log('Operación registrada en la base de datos.');
            },
            error: function (xhr, status, error) {
                console.log('Error al registrar la operación en la base de datos.');
            }
        });

    }

    document.getElementById("displayBox").value = result;
    this.operationString = "";
    this.operationString += result;
    this.justSolved = true;

    return result;
}



/**

Limpia el contenido de la pantalla y la cadena de operaciones.
*/
CalculadoraCientifica.prototype.clearDisplay = function () {
    CalculadoraBasica.prototype.clearDisplay.call(this);
    this.operationString = "";
}


/**

Cambia el signo del número en la pantalla y actualiza la cadena de operaciones.
*/
CalculadoraCientifica.prototype.toggleSign = function () {
    var displayBox = document.getElementById("displayBox");
    var displayContents = displayBox.value;
    if (displayContents == "Syntax Error") {
        CalculadoraBasica.prototype.clearDisplay.call(this);
    }
    var num = parseFloat(displayContents);
    num *= -1;
    displayBox.value = num.toString();
    this.operationString = num.toString();
}


/**

Limpia el contenido de la memoria.
*/
CalculadoraCientifica.prototype.clearMemory = function () {
    CalculadoraBasica.prototype.subtractFromMemory.call(this);
}


/**

Muestra el contenido de la memoria en la pantalla.
*/
CalculadoraCientifica.prototype.readMemory = function () {
    this.writeToDisplay(this.memoryRegister);
}


/**

Guarda el resultado de la operación actual en la memoria.
*/
CalculadoraCientifica.prototype.saveToMemory = function () {
    this.memoryRegister = this.solveOperation();
}


/**

Borra el último caracter ingresado en la pantalla y actualiza la cadena de operaciones.
*/
CalculadoraCientifica.prototype.eraseLastInput = function () {
    var currentDisplay = document.getElementById("displayBox").value;
    if (currentDisplay.length > 1) {
        var newDisplay = currentDisplay.substring(0, currentDisplay.length - 1);
        document.getElementById("displayBox").value = newDisplay;
        this.operationString = newDisplay;
    } else {
        document.getElementById("displayBox").value = "0";
        this.operationString = "0";
    }
}


/**

Escribe una función matemática en la pantalla y actualiza la cadena de operaciones.
@param {string} data - Función matemática a escribir en la pantalla.
*/
CalculadoraCientifica.prototype.writeMathFunction = function (data) {
    if (document.getElementById("displayBox").value == "Syntax Error") {
        CalculadoraBasica.prototype.clear
            ();
    }
    CalculadoraBasica.prototype.writeToDisplay.call(this, data);
    this.operationString += this.operationMap[data];
    this.inputList.push(data);
}


/**
 * Calcula el factorial de un número y muestra el resultado en la pantalla.
 */
CalculadoraCientifica.prototype.calculateFactorial = function () {
    var number = parseInt(this.operationString.split(new RegExp("[^0-9]")));
    var result = 0;
    try {
        result = this.calculateRecursiveFactorial(number);

        // Hacer la llamada AJAX
        this.registerOperation(`Factorial(${number})`, result);
    } catch (err) {
        document.getElementById("displayBox").value = "Numero demasiado grande";
        return;
    }
    this.clearDisplay();
    document.getElementById("displayBox").value = result;
};

CalculadoraCientifica.prototype.calculateRecursiveFactorial = function (number) {
    if (number == 1 || number == 0) {
        return 1;
    }
    return number * this.calculateRecursiveFactorial(number - 1);
};


/**
 * Calcula la potencia de diez de un número y muestra el resultado en la pantalla.
 */
CalculadoraCientifica.prototype.nthTenPower = function () {
    var number = parseInt(this.operationString.split(new RegExp("[^0-9]")));
    var result = Math.pow(10, parseInt(number));

    // Hacer la llamada AJAX
    this.registerOperation(`Math.pow(10, ${number})`, result);

    this.clearDisplay();
    this.writeToDisplay(result);
};



/**

Calcula cuadrado de un número y muestra el resultado en la pantalla.
*/
CalculadoraCientifica.prototype.square = function () {
    var number = parseFloat(this.operationString);
    var result = Math.pow(number, 2);
    this.registerOperation(`Math.pow(${number}, 2)`, result);
    this.clearDisplay();
    this.writeToDisplay(result);
    this.operationString = result.toString();
}


/**
 * Calcula el cubo de un número y muestra el resultado en la pantalla.
 */
CalculadoraCientifica.prototype.cube = function () {
    var number = parseFloat(this.operationString);
    var result = Math.pow(number, 3);

    // Hacer la llamada AJAX
    this.registerOperation(`Math.pow(${number}, 3)`, result);

    this.clearDisplay();
    this.writeToDisplay(result);
    this.operationString = result.toString();
};

/**
 * Calcula el inverso de un número y muestra el resultado en la pantalla.
 */
CalculadoraCientifica.prototype.inverseNumber = function () {
    var number = parseInt(this.operationString.split(new RegExp("[^0-9]")));
    var result = Math.pow(parseInt(number), -1);

    // Hacer la llamada AJAX
    this.registerOperation(`Math.pow(${number}, -1)`, result);

    this.clearDisplay();
    this.writeToDisplay(result);
};



const calculadora = new CalculadoraCientifica();  

CalculadoraCientifica.prototype.registerOperation = function (operationString, result) {
    $.ajax({
        type: 'POST',
        url: '/Calculadora/RegisterOperation',
        data: { OperationString: operationString, Resultado: result },
        success: function (data) {
            console.log('Operación registrada en la base de datos.');
        },
        error: function (xhr, status, error) {
            console.log('Error al registrar la operación en la base de datos.');
        }
    });
};
