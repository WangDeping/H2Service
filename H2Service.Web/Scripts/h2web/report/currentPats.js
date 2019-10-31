    var patsDashChart = echarts.init(document.getElementById('gauge'));
    var dynamicInPat_BarChart = echarts.init(document.getElementById('dynamicInPat'));
    function InitPatsDashChart() {
        $.get(CURRENTPATS_URL, function (result) {
            dynamicInPat_BarChart.setOption({
                tooltip: {},
                title: {
                    text: "科室当前在院人数"
                },
                grid: {
                    left: '0%',
                    right: '0%',
                    bottom: '5%',
                    containLabel: true
                },
                xAxis: {
                    data: result.Deps,
                    axisLabel: {
                        interval: 0,
                        rotate: 45
                    }
                },
                yAxis: {},
                series: [{
                    // 根据名字对应到相应的系列
                    name: '在院人数',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#2f4554'
                        }
                    },
                    data: result.DepInpats,
                    label: {
                        show: true
                    }
                }]

            })
            option = {
                tooltip: {
                    formatter: "{a} <br/>{c} {b}"
                },
                grid: {
                    left: '0%',
                    right: '4%',
                    bottom: '0%',
                    containLabel: true
                },
                toolbox: {
                    show: false
                },
                series: [
                    {
                        name: '人次',
                        type: 'gauge',
                        z: 3,
                        min: 0,
                        max: 600,
                        splitNumber: 12,
                        center: ['50%', '45%'],
                        radius: '90%',
                        axisLine: {            // 坐标轴线
                            lineStyle: {       // 属性lineStyle控制线条样式
                                width: 10
                            }
                        },
                        axisTick: {            // 坐标轴小标记
                            length: 15,        // 属性length控制线长
                            lineStyle: {       // 属性lineStyle控制线条样式
                                color: 'auto'
                            }
                        },
                        splitLine: {           // 分隔线
                            length: 20,         // 属性length控制线长
                            lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                                color: 'auto'
                            }
                        },
                        axisLabel: {
                            backgroundColor: 'auto',
                            borderRadius: 2,
                            color: '#eee',
                            padding: 3,
                            textShadowBlur: 2,
                            textShadowOffsetX: 1,
                            textShadowOffsetY: 1,
                            textShadowColor: '#222'
                        },
                        title: {
                            // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                            fontWeight: 'bolder',
                            fontSize: 20,
                            fontStyle: 'italic'
                        },
                        detail: {
                            // 其余属性默认使用全局文本样式，详见TEXTSTYLE

                            fontWeight: 'bolder',
                            borderRadius: 3,
                            backgroundColor: '#444',
                            borderColor: '#aaa',
                            shadowBlur: 5,
                            shadowColor: '#333',
                            shadowOffsetX: 0,
                            shadowOffsetY: 3,
                            borderWidth: 2,
                            textBorderColor: '#000',
                            textBorderWidth: 2,
                            textShadowBlur: 2,
                            textShadowColor: '#fff',
                            textShadowOffsetX: 0,
                            textShadowOffsetY: 0,
                            fontFamily: 'Arial',
                            width: 100,
                            color: '#eee',
                            rich: {}
                        },
                        data: [{ value: result.InPats, name: '在院人数' }]
                    },
                    {
                        name: '人次',
                        type: 'gauge',
                        z: 3,
                        min: 0,
                        max: 220,
                        splitNumber: 11,
                        radius: '90%',
                        center: ['80%', '45%'],
                        axisLine: {            // 坐标轴线
                            lineStyle: {       // 属性lineStyle控制线条样式
                                width: 10
                            }
                        },
                        axisTick: {            // 坐标轴小标记
                            length: 15,        // 属性length控制线长
                            lineStyle: {       // 属性lineStyle控制线条样式
                                color: 'auto'
                            }
                        },
                        splitLine: {           // 分隔线
                            length: 20,         // 属性length控制线长
                            lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                                color: 'auto'
                            }
                        },
                        axisLabel: {
                            backgroundColor: 'auto',
                            borderRadius: 2,
                            color: '#eee',
                            padding: 3,
                            textShadowBlur: 2,
                            textShadowOffsetX: 1,
                            textShadowOffsetY: 1,
                            textShadowColor: '#222'
                        },
                        title: {
                            // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                            fontWeight: 'bolder',
                            fontSize: 20,
                            fontStyle: 'italic'
                        },
                        detail: {
                            // 其余属性默认使用全局文本样式，详见TEXTSTYLE

                            fontWeight: 'bolder',
                            borderRadius: 3,
                            backgroundColor: '#444',
                            borderColor: '#aaa',
                            shadowBlur: 5,
                            shadowColor: '#333',
                            shadowOffsetX: 0,
                            shadowOffsetY: 3,
                            borderWidth: 2,
                            textBorderColor: '#000',
                            textBorderWidth: 2,
                            textShadowBlur: 2,
                            textShadowColor: '#fff',
                            textShadowOffsetX: 0,
                            textShadowOffsetY: 0,
                            fontFamily: 'Arial',
                            width: 100,
                            color: '#eee',
                            rich: {}
                        },
                        data: [{ value: result.NewPats, name: '入院人数' }]
                    },
                    {
                        name: '人次',
                        type: 'gauge',
                        z: 3,
                        min: 0,
                        max: 220,
                        splitNumber: 11,
                        radius: '90%',
                        center: ['20%', '45%'],
                        axisLine: {            // 坐标轴线
                            lineStyle: {       // 属性lineStyle控制线条样式
                                width: 10
                            }
                        },
                        axisTick: {            // 坐标轴小标记
                            length: 15,        // 属性length控制线长
                            lineStyle: {       // 属性lineStyle控制线条样式
                                color: 'auto'
                            }
                        },
                        splitLine: {           // 分隔线
                            length: 20,         // 属性length控制线长
                            lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                                color: 'auto'
                            }
                        },
                        axisLabel: {
                            backgroundColor: 'auto',
                            borderRadius: 2,
                            color: '#eee',
                            padding: 3,
                            textShadowBlur: 2,
                            textShadowOffsetX: 1,
                            textShadowOffsetY: 1,
                            textShadowColor: '#222'
                        },
                        title: {
                            // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                            fontWeight: 'bolder',
                            fontSize: 20,
                            fontStyle: 'italic'
                        },
                        detail: {
                            fontWeight: 'bolder',
                            borderRadius: 3,
                            backgroundColor: '#444',
                            borderColor: '#aaa',
                            shadowBlur: 5,
                            shadowColor: '#333',
                            shadowOffsetX: 0,
                            shadowOffsetY: 3,
                            borderWidth: 2,
                            textBorderColor: '#000',
                            textBorderWidth: 2,
                            textShadowBlur: 2,
                            textShadowColor: '#fff',
                            textShadowOffsetX: 0,
                            textShadowOffsetY: 0,
                            fontFamily: 'Arial',
                            width: 100,
                            color: '#eee',
                            rich: {}
                        },
                        data: [{ value: result.OutPats, name: '出院人数' }]
                    }

                ]
            };
            patsDashChart.setOption(option);


        })
    }
    InitPatsDashChart();
    setInterval(function () {
        InitPatsDashChart();
    }, 1000 * 60 * 5);


