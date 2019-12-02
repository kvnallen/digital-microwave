(() => {

    let originalInput = '';

    init();


    ////////////////////////////

    function init() {
        toggleParamFields({ disabled: true, disableButtons: true });
        initEvents();
        connect();
    }

    function initEvents() {
        $('#start-button').on('click', start);
        $('#fast-start-button').on('click', fastStart);
        $('#pause-button').on('click', pause);
        $('#cancel-button').on('click', cancel);
        $('#microwave-programs').on('change', onChangeProgram);
        $('#timer-input').on('keydown', () => originalInput = '')
    }

    function onChangeProgram() {
        const programName = $(this).val();

        if (programName) {
            toggleParamFields({ disabled: true });
        } else {
            toggleParamFields({ disabled: false });
            return;
        }

        fetch(`/program/${programName}`)
            .then(res => res.json())
            .then(program => {
                const { time, power, instructions, heatingCharacter } = program;
                $('#Time').val(time);
                $('#Power').val(power);
                $('#program-instructions').val(instructions);
                $('#program-heating-character').val(heatingCharacter);
            });
    }

    function toggleParamFields({ disabled, disableButtons = false }) {
        $('#Power').attr('disabled', disabled);
        $('#Time').attr('disabled', disabled);

        $('#program-container').toggleClass('hide', !disabled);
        $('.action-button').attr('disabled', disableButtons);
    }

    async function connect() {
        try {
            const connection = new signalR.HubConnectionBuilder()
                .withUrl("/timer")
                .build();

            await connection.start();
            setTimeout(() => toggleParamFields({ disabled: false, disableButtons: false }), 1000);
            $('#connecting-microwave').hide();
            setEvents(connection);

            return connection;

        } catch (err) {
            setTimeout(() => connect(), 1000);
        }
    };

    function setEvents(connection) {

        connection.onclose(
            async () => await connect());

        connection.on("timerUpdated",
            (message) => updateState({ working: true, message }));

        connection.on("timerFinished",
            (message) => updateState({ working: false, message }));

        connection.on("timerPaused",
            (message, currentTime) => updateState({ working: false, message, currentTime }));

        connection.on("timerCancelled",
            (message) => updateState({ working: false, message: '' }));
    }

    function updateState({ working, message, currentTime }) {
        $('#fast-start-button').attr('disabled', working);
        $('#start-button').attr('disabled', working);

        $('#timer-input')
            .val(message)
            .attr('disabled', working);

        $('#current-time').val(currentTime);

        $('#microwave-programs').attr('disabled', working)
    }

    function start() {
        const data = getOptions();
        sendPost({ url: '/microwave/start', data })
    }

    function fastStart() {
        const data = getOptions();
        sendPost({ url: '/microwave/fast-start', data })
    }

    function pause() {
        sendPost({ url: '/microwave/pause' });
    }

    function cancel() {
        sendPost({ url: '/microwave/cancel' });
    }

    function getOptions() {
        const text = originalInput || $('#timer-input').val();

        if (originalInput) {
            $('#timer-input').val(originalInput);
        } else {
            originalInput = text;
        }

        return {
            time: Number($('#Time').val()),
            power: Number($('#Power').val()),
            text: text,
            programName: $('#microwave-programs').val()
        }
    }

    function handleErrors(errors) {
        const errorsHtml = errors.map(error => `<li>${error.value}</li>`).join();
        $('#errors')
            .html(`<ul>${errorsHtml}</ul>`)
            .removeClass('hide');

        setTimeout(() => $('#errors').addClass('hide'), 10000);
    }

    function sendPost({ url, data }) {
        return fetch(url,
                {
                    method: 'POST',
                    body: JSON.stringify(data),
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                })
            .then(response => response.json())
            .then(error => handleErrors(error));
    }

})();